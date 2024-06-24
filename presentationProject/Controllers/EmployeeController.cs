using AutoMapper;
using BLL.Repos.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using presentationProject.Utility;
using presentationProject.ViewModels;
using System.Collections;

namespace presentationProject.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepo _employee;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _imageName;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                 employees = await _unitOfWork.Employees.GetALLAsync();
                return View(_mapper.Map<IEnumerable<EmployeeVm>>(employees));
            }
            employees =await _unitOfWork.Employees.GetAllAsync( e=> e.Name.ToLower().Contains(searchValue.ToLower()));

            return View(_mapper.Map<IEnumerable<EmployeeVm>>(employees));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeVm employeeVm)
        {
            if (ModelState.IsValid)
            {
                employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image,"Images");
                var employee = _mapper.Map<EmployeeVm, Employee>(employeeVm);
                await _unitOfWork.Employees.AddAsync(employee);
                
                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    TempData["Success"] = "Employee is Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employeeVm);
        }

        public async Task<IActionResult> Details(int? id) =>await ReturnViewWithEmployee(id, nameof(Details));

        public async Task<IActionResult> Edit(int? id) => await ReturnViewWithEmployee(id, nameof(Edit));

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EmployeeVm employeeVm, [FromRoute] int id)
        {
            if (id != employeeVm.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVm.Image != null)
                    {
                        employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
                    }  
                    _unitOfWork.Employees.Update(_mapper.Map<EmployeeVm, Employee>(employeeVm));
                   await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employeeVm);
        }

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete([FromRoute] int? id)
        {

            if (!id.HasValue)
                return BadRequest();

            var employee = await _unitOfWork.Employees.GetAsync(id.Value);

            if (employee == null)
                return NotFound();

            _unitOfWork.Employees.Delete(_mapper.Map<Employee>(employee));

            if (await _unitOfWork.CompleteAsync() > 0)
                DocumentSettings.DeleteFile(employee.ImageName, "Images");

            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ReturnViewWithEmployee(int? id, string viewName)
        {
            if (!id.HasValue)
                return BadRequest();

            var employee = await _unitOfWork.Employees.GetAsync(id.Value);

            if (employee == null)
                return NotFound();

            return View(viewName, _mapper.Map<EmployeeVm>(employee));

        }
    }
}

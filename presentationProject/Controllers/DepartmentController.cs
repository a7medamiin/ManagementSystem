using AutoMapper;
using BLL.Repos.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using presentationProject.ViewModels;

namespace presentationProject.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepo _department;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork,IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<Department> departments;
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                departments = await _unitOfWork.Departments.GetALLAsync();
                return View(_mapper.Map<IEnumerable<DepartmentVm>>(departments));
            }

            IEnumerable<Department> enumerable = await _unitOfWork.Departments.GetAllAsync(e => e.Name.ToLower().Contains(searchValue.ToLower()));
            departments = enumerable;

            return View(_mapper.Map<IEnumerable<DepartmentVm>>(departments));
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DepartmentVm departmentVm)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Departments.AddAsync(_mapper.Map<Department>(departmentVm));
               await _unitOfWork.CompleteAsync();
                //if ( > 0)
                TempData["Success"] = "Department is Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVm);
        }

        public async Task<IActionResult> Details(int? id) => await ReturnViewWithDepartment(id, nameof(Details));

        public async Task<IActionResult> Edit(int? id) => await ReturnViewWithDepartment(id, nameof(Edit));

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditAsync(DepartmentVm departmentVm, [FromRoute] int id)
        {
            if (id != departmentVm.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Departments.Update(_mapper.Map<Department>(departmentVm));
                   await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(departmentVm);
        }

        [HttpPost]
		public async Task<IActionResult> Delete(int? id) 
        {
            if (!id.HasValue)
                return BadRequest();

            var department = await _unitOfWork.Departments.GetAsync(id.Value);

            if (department == null)
                return NotFound();

            _unitOfWork.Departments.Delete(_mapper.Map<Department>(department));
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ReturnViewWithDepartment(int? id, string viewName)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = await _unitOfWork.Departments.GetAsync(id.Value);

            if (department == null)
                return NotFound();

            return View(viewName , _mapper.Map<DepartmentVm>(department));

        }
    }
}

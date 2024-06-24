using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using presentationProject.ViewModels;

namespace presentationProject.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManage;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManage = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManage.Roles.Select(r => new RoleVm
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToListAsync();
                return View(roles);
            }
            var role = await _roleManage.FindByNameAsync(name);
            if (role == null)
            {
                return View(Enumerable.Empty<RoleVm>());
            }
            var mappedRole = new RoleVm
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(new List<RoleVm> { mappedRole });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleVm model)
        {
            if (!ModelState.IsValid) return View(model);

            var mappedRole = _mapper.Map<IdentityRole>(model);
            var result = await _roleManage.CreateAsync(mappedRole);
            if (result.Succeeded) return RedirectToAction(nameof(Index));
            foreach (var role in result.Errors)
            {
                ModelState.AddModelError("", role.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> Details(string id, string viewName = nameof(Details))
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var role = await _roleManage.FindByIdAsync(id);

            if (role == null) return NotFound();

            var mappedRole = _mapper.Map<RoleVm>(role);

            return View(viewName, mappedRole);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, nameof(Edit));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleVm model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid) return View(model);

            try
            {
                var role = await _roleManage.FindByIdAsync(id);
                if (role == null) return NotFound();

                role.Name = model.Name;

                await _roleManage.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManage.FindByIdAsync(id);

            if (role == null) return NotFound();

            await _roleManage.DeleteAsync(role);

            return RedirectToAction(nameof(Index));
        }
    }
}

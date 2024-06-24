using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using presentationProject.ViewModels;

namespace presentationProject.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				var users = await _userManager.Users.Select(x => new UserVm
				{
					Id = x.Id,
					Email = x.Email,
					Fname = x.FName,
					Lname = x.LName,
					Roles = _userManager.GetRolesAsync(x).Result
				}).ToListAsync();
				return View(users);
			}
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return View(Enumerable.Empty<UserVm>());
			}
			var mappedUser = new UserVm
			{
				Id = user.Id,
				Email = user.Email,
				Fname = user.FName,
				Lname = user.LName,
				Roles = _userManager.GetRolesAsync(user).Result
			};
			return View(new List<UserVm> { mappedUser});
		}

		public async Task<IActionResult> Details(string id, string viewName = nameof(Details))
		{
			if (string.IsNullOrEmpty(id)) return BadRequest();

			var user = await _userManager.FindByIdAsync(id);

			if (user == null) return NotFound();

			var mappedUser = _mapper.Map<UserVm>(user);
			mappedUser.Roles = await _userManager.GetRolesAsync(user);

			return View(viewName,mappedUser);
		}

		public async Task<IActionResult> Edit(string id)
		{
			return await Details( id, nameof(Edit));
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, UserVm model)
		{
			if (id != model.Id) return BadRequest();

			if (!ModelState.IsValid) return View(model);

			try
			{
				var user = await _userManager.FindByIdAsync(id);
				if (user == null) return NotFound();

				user.FName = model.Fname;
				user.LName = model.Lname;

				await _userManager.UpdateAsync(user);
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
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}

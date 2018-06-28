using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PieShop.Auth;
using PieShop.ViewModels.Admin;

namespace PieShop.Controllers
{
	[Authorize(Roles = "Administrators")]
	public class AdminController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AdminController(
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index() =>
			View();

		public IActionResult UserManagement() =>
			View(_userManager.Users);

		public IActionResult AddUser() =>
			View();

		[HttpPost]
		public async Task<IActionResult> AddUser([FromForm]AddUserViewModel addUserViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var user = new ApplicationUser
			{
				UserName = addUserViewModel.UserName,
				Email = addUserViewModel.Email,
				BirthDate = addUserViewModel.Birthdate,
				City = addUserViewModel.City,
				Country = addUserViewModel.Country
			};

			IdentityResult identityResult = await _userManager.CreateAsync(user, addUserViewModel.Password);

			if (identityResult.Succeeded)
			{
				return RedirectToAction(nameof(UserManagement));
			}

			foreach (var identityResultError in identityResult.Errors)
			{
				ModelState.AddModelError("", identityResultError.Description);
			}

			return View();
		}

		public async Task<IActionResult> EditUser(string userId)
		{
			ApplicationUser ApplicationUser = await _userManager.FindByIdAsync(userId);

			if (ApplicationUser == null)
			{
				return RedirectToAction(nameof(UserManagement));
			}

			return View(ApplicationUser);
		}

		[HttpPost]
		public async Task<IActionResult> EditUser([FromForm]EditUserViewModel editUserViewModel)
		{
			ApplicationUser applicationUser = await _userManager.FindByIdAsync(editUserViewModel.Id);

			if (applicationUser == null)
			{
				return RedirectToAction(nameof(UserManagement));
			}

			applicationUser.UserName = editUserViewModel.UserName;
			applicationUser.Email = editUserViewModel.Email;
			applicationUser.BirthDate = editUserViewModel.Birthdate;
			applicationUser.City = editUserViewModel.City;
			applicationUser.Country = editUserViewModel.Country;

			IdentityResult identityResult = await _userManager.UpdateAsync(applicationUser);

			if (identityResult.Succeeded)
			{
				return RedirectToAction(nameof(UserManagement));
			}

			foreach (var identityResultError in identityResult.Errors)
			{
				ModelState.AddModelError("", identityResultError.Description);
			}

			return View(applicationUser);
		}


		[HttpPost]
		public async Task<IActionResult> DeleteUser(string userId)
		{
			ApplicationUser ApplicationUser = await _userManager.FindByIdAsync(userId);

			if (ApplicationUser == null)
			{
				ModelState.AddModelError("", "This user can't be found");
				return RedirectToAction(nameof(UserManagement));
			}

			IdentityResult identityResult = await _userManager.DeleteAsync(ApplicationUser);

			if (identityResult.Succeeded)
			{
				return RedirectToAction(nameof(UserManagement));
			}

			foreach (var identityResultError in identityResult.Errors)
			{
				ModelState.AddModelError("", identityResultError.Description);
			}

			return RedirectToAction(nameof(UserManagement));
		}

		public IActionResult RoleManagement() =>
			View(_roleManager.Roles);

		public IActionResult AddRole() =>
			View();

		[HttpPost]
		public async Task<IActionResult> AddRole([FromForm]AddRoleViewModel addUserViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var identityRole = new IdentityRole(addUserViewModel.RoleName);

			IdentityResult identityResult = await _roleManager.CreateAsync(identityRole);

			if (identityResult.Succeeded)
			{
				return RedirectToAction(nameof(RoleManagement));
			}

			foreach (var identityResultError in identityResult.Errors)
			{
				ModelState.AddModelError("", identityResultError.Description);
			}

			return View();
		}

		public async Task<IActionResult> EditRole(string id)
		{
			IdentityRole identityRole = await _roleManager.FindByIdAsync(id);

			if (identityRole == null)
			{
				return RedirectToAction(nameof(RoleManagement));
			}

			var editRoleViewModel = new EditRoleViewModel
			{
				Id = identityRole.Id,
				RoleName = identityRole.Name,
				Users = new List<string>()
			};

			foreach (var applicationUser in _userManager.Users)
			{
				if (await _userManager.IsInRoleAsync(applicationUser, identityRole.Name))
				{
					editRoleViewModel.Users.Add(applicationUser.UserName);
				}
			}

			return View(editRoleViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> EditRole([FromForm]EditRoleViewModel editRoleViewModel)
		{
			IdentityRole identityRole = await _roleManager.FindByIdAsync(editRoleViewModel.Id);

			if (identityRole == null)
			{
				ModelState.AddModelError(string.Empty, "Role not found");
				return RedirectToAction(nameof(RoleManagement));
			}

			identityRole.Name = editRoleViewModel.RoleName;

			IdentityResult identityResult = await _roleManager.UpdateAsync(identityRole);

			if (identityResult.Succeeded)
			{
				return RedirectToAction(nameof(RoleManagement));
			}

			foreach (var identityResultError in identityResult.Errors)
			{
				ModelState.AddModelError("", identityResultError.Description);
			}

			return RedirectToAction(nameof(RoleManagement));
		}

		[HttpPost]
		public async Task<IActionResult> DeleteRole(string id)
		{
			IdentityRole identityRole = await _roleManager.FindByIdAsync(id);

			if (identityRole == null)
			{
				ModelState.AddModelError(string.Empty, "Role not found");
				return RedirectToAction(nameof(RoleManagement));
			}

			IdentityResult identityResult = await _roleManager.DeleteAsync(identityRole);

			if (identityResult.Succeeded)
			{
				return RedirectToAction(nameof(RoleManagement));
			}

			foreach (var identityResultError in identityResult.Errors)
			{
				ModelState.AddModelError("", identityResultError.Description);
			}

			return RedirectToAction(nameof(RoleManagement));
		}

		public async Task<IActionResult> AddUserToRole(string roleId)
		{
			IdentityRole identityRole = await _roleManager.FindByIdAsync(roleId);

			if (identityRole == null)
			{
				ModelState.AddModelError(string.Empty, "Role not found");
				return RedirectToAction(nameof(RoleManagement));
			}

			var addUserToRoleViewModel = new AddUserToRoleViewModel { RoleId = roleId };

			foreach (var applicationUser in _userManager.Users)
			{
				if (!await _userManager.IsInRoleAsync(applicationUser, identityRole.Name))
				{
					addUserToRoleViewModel.Users.Add(applicationUser);
				}
			}

			return View(addUserToRoleViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> AddUserToRole([FromForm]string roleId, [FromForm]string userId)
		{
			IdentityRole identityRole = await _roleManager.FindByIdAsync(roleId);
			ApplicationUser applicationUser = await _userManager.FindByIdAsync(userId);

			IdentityResult identityResult = await _userManager.AddToRoleAsync(applicationUser, identityRole.Name);

			if (identityResult.Succeeded)
			{
				return RedirectToAction(nameof(RoleManagement));
			}

			foreach (var identityResultError in identityResult.Errors)
			{
				ModelState.AddModelError("", identityResultError.Description);
			}

			return RedirectToAction(nameof(RoleManagement));
		}

		public async Task<IActionResult> DeleteUserFromRole(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);

			if (role == null)
				return RedirectToAction("RoleManagement", _roleManager.Roles);

			var addUserToRoleViewModel = new DeleteUserFromRoleViewModel { RoleId = role.Id };

			foreach (var user in _userManager.Users)
			{
				if (await _userManager.IsInRoleAsync(user, role.Name))
				{
					addUserToRoleViewModel.Users.Add(user);
				}
			}

			return View(addUserToRoleViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUserFromRole([FromForm]DeleteUserFromRoleViewModel userRoleViewModel)
		{
			var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
			var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);

			var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

			if (result.Succeeded)
			{
				return RedirectToAction("RoleManagement", _roleManager.Roles);
			}

			foreach (IdentityError error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}

			return View(userRoleViewModel);
		}
	}
}

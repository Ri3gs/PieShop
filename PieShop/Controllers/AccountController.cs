using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PieShop.ViewModels.Account;

namespace PieShop.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

		public AccountController(
			SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromForm]LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

				if (user != null)
				{
					var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
				}
			}

			ModelState.AddModelError("", "User name/password not found");
			return Login();
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromForm]RegisterViewModel registerViewModel)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser(registerViewModel.UserName);
				var result = await _userManager.CreateAsync(user, registerViewModel.Password);

				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
			}

			return Register();
		}
	}
}

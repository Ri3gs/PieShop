using Microsoft.AspNetCore.Mvc;

namespace PieShop.Controllers
{
	public class AppExceptionController : Controller
	{
		public IActionResult Index() => View();
	}
}
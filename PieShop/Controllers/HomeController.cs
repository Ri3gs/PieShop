using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels.Home;

namespace PieShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPieRepository _pieRepository;

		public HomeController(IPieRepository pieRepository)
		{
			_pieRepository = pieRepository;
		}

		public IActionResult Index() =>
			View(new IndexViewModel { Title = "Here are our pies of the week!", Pies = _pieRepository.GetPiesOfTheWeek() });
	}
}

using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PieShop.Models;
using PieShop.ViewModels.Pie;

namespace PieShop.Controllers
{
	[Authorize(Roles = "Administrators")]
	public class PieManagementController : Controller
	{
		private readonly IPieRepository _pieRepository;
		private readonly ICategoryRepository _categoryRepository;

		public PieManagementController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
		{
			_pieRepository = pieRepository;
			_categoryRepository = categoryRepository;
		}

		public ViewResult Index()
		{
			var pies = _pieRepository.GetAll().OrderBy(p => p.Id);
			return View(pies);
		}

		public IActionResult AddPie()
		{
			var categories = _categoryRepository.Categories;
			var pieEditViewModel = new PieEditViewModel
			{
				Categories = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.Id.ToString() }).ToList(),
				CategoryId = categories.FirstOrDefault().Id
			};
			return View(pieEditViewModel);
		}

		[HttpPost]
		public IActionResult AddPie(PieEditViewModel pieEditViewModel)
		{
			//Basic validation
			if (ModelState.IsValid)
			{
				_pieRepository.CreatePie(pieEditViewModel.Pie);
				return RedirectToAction("Index");
			}
			return View(pieEditViewModel);
		}

		public IActionResult EditPie(int pieId)
		{
			var categories = _categoryRepository.Categories;

			var pie = _pieRepository.GetAll().FirstOrDefault(p => p.Id == pieId);

			var pieEditViewModel = new PieEditViewModel
			{
				Categories = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.Id.ToString() }).ToList(),
				Pie = pie,
				CategoryId = pie.CategoryId
			};

			var item = pieEditViewModel.Categories.FirstOrDefault(c => c.Value == pie.CategoryId.ToString());
			item.Selected = true;

			return View(pieEditViewModel);
		}

		[HttpPost]
		public IActionResult EditPie(PieEditViewModel pieEditViewModel)
		{
			pieEditViewModel.Pie.CategoryId = pieEditViewModel.CategoryId;

			if (ModelState.IsValid)
			{
				_pieRepository.UpdatePie(pieEditViewModel.Pie);
				return RedirectToAction("Index");
			}
			return View(pieEditViewModel);
		}

		[HttpPost]
		public IActionResult DeletePie(string pieId)
		{
			return View();
		}
	}
}
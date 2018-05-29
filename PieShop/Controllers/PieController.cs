using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels.Pie;

namespace PieShop.Controllers
{
	public class PieController : Controller
	{
		private readonly IPieRepository _pieRepository;
		private readonly ICategoryRepository _categoryRepository;

		public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
		{
			_pieRepository = pieRepository;
			_categoryRepository = categoryRepository;
		}

		public IActionResult List(string category)
		{
			string defaultCategory = "All pies";

			if (string.IsNullOrWhiteSpace(category))
			{
				return View(new PiesListViewModel
				{
					CurrentCategory = defaultCategory,
					Pies = _pieRepository.GetAll()
				});
			}

			return View(new PiesListViewModel
				{
					CurrentCategory = category,
					Pies = _pieRepository.GetAll().Where(pie => pie.Category.CategoryName == category)
				});
		}

		public IActionResult Details(int id)
		{
			Pie pie = _pieRepository.GetById(id);

			if (pie == null)
			{
				return NotFound();
			}

			return View(pie);
		}
	}
}

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;

namespace PieShop.Components
{
	public class CategoryMenu : ViewComponent
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryMenu(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public IViewComponentResult Invoke()
		{
			return View(_categoryRepository.Categories.OrderBy(category => category.CategoryName));
		}
	}
}
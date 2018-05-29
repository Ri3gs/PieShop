using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels.ShoppingCart;

namespace PieShop.Controllers
{
	public class ShoppingCartController : Controller
	{
		private readonly IPieRepository _pieRepository;
		private readonly ShoppingCart _shoppingCart;

		public ShoppingCartController(IPieRepository pieRepository, ShoppingCart shoppingCart)
		{
			_pieRepository = pieRepository;
			_shoppingCart = shoppingCart;
		}

		public IActionResult Index()
		{
			List<ShoppingCartItem> shoppingCartItems = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = shoppingCartItems;

			var shoppingCartViewModel = new ShoppingCartViewModel
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
			};

			return View(shoppingCartViewModel);
		}

		public IActionResult AddToShoppingCart(int pieId)
		{
			Pie selectedPie = _pieRepository.GetById(pieId);

			if (selectedPie != null)
			{
				_shoppingCart.AddToCart(selectedPie, 1);
			}

			return RedirectToAction(nameof(Index));
		}

		public IActionResult RemoveFromShoppingCart(int pieId)
		{
			Pie selectedPie = _pieRepository.GetById(pieId);

			if (selectedPie != null)
			{
				_shoppingCart.RemoveFromCart(selectedPie);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
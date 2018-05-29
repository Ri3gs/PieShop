using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels.ShoppingCart;

namespace PieShop.Components
{
	public class ShoppingCartSummary : ViewComponent
	{
		private readonly ShoppingCart _shoppingCart;

		public ShoppingCartSummary(ShoppingCart shoppingCart)
		{
			_shoppingCart = shoppingCart;
		}

		public IViewComponentResult Invoke()
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
	}
}
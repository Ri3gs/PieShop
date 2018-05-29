using System;

namespace PieShop.Models
{
	public class OrderRepository : IOrderRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ShoppingCart _shoppingCart;

		public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
		{
			_appDbContext = appDbContext;
			_shoppingCart = shoppingCart;
		}

		public void CreateOrder(Order order)
		{
			order.OrderPlaced = DateTime.Now;

			_appDbContext.Orders.Add(order);

			var shoppingCartItems = _shoppingCart.ShoppingCartItems;

			foreach (var shoppingCartItem in shoppingCartItems)
			{
				var orderDetail = new OrderLine
				{
					Amount = shoppingCartItem.Amount,
					PieId = shoppingCartItem.Pie.Id,
					OrderId = order.Id,
					Price = shoppingCartItem.Pie.Price
				};

				_appDbContext.OrderLines.Add(orderDetail);
			}

			_appDbContext.SaveChanges();
		}
	}
}
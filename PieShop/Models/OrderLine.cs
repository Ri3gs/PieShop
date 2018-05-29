namespace PieShop.Models
{
	public class OrderLine
	{
		public int Id { get; set; }
		public int Amount { get; set; }
		public decimal Price { get; set; }
		public int PieId { get; set; }
		public virtual Pie Pie { get; set; }
		public int OrderId { get; set; }
		public virtual Order Order { get; set; }
	}
}
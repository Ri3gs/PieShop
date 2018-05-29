using System.Collections.Generic;

namespace PieShop.ViewModels.Home
{
	public class IndexViewModel
	{
		public string Title { get; set; }
		public IEnumerable<Models.Pie> Pies { get; set; }
	}
}

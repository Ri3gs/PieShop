using System.Collections.Generic;

namespace PieShop.ViewModels.Pie
{
	public class PiesListViewModel
	{
		public string CurrentCategory { get; set; }
		public IEnumerable<Models.Pie> Pies { get; set; }
	}
}

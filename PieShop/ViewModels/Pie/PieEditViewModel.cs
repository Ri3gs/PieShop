using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PieShop.ViewModels.Pie
{
	public class PieEditViewModel
	{
		public List<SelectListItem> Categories { get; set; }
		public int CategoryId { get; set; }
		public Models.Pie Pie { get; set; }
	}
}
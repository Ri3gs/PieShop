using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PieShop.ViewModels.Admin
{
	public class EditRoleViewModel
	{
		public string Id { get; set; }

		[Required]
		[MinLength(3)]
		[Display(Name = "Role name")]
		public string RoleName { get; set; }

		public List<string> Users { get; set; }
	}
}
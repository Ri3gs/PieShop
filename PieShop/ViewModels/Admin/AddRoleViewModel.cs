using System.ComponentModel.DataAnnotations;

namespace PieShop.ViewModels.Admin
{
	public class AddRoleViewModel
	{
		[Required]
		[MinLength(3)]
		[Display(Name = "Role name")]
		public string RoleName { get; set; }
	}
}
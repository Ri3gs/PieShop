using System.Collections.Generic;
using PieShop.Auth;

namespace PieShop.ViewModels.Admin
{
	public class AddUserToRoleViewModel
	{
		public AddUserToRoleViewModel()
		{
			Users = new List<ApplicationUser>();
		}

		public string UserId { get; set; }
		public string RoleId { get; set; }
		public List<ApplicationUser> Users { get; set; }
	}
}
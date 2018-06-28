using System.Collections.Generic;
using PieShop.Auth;

namespace PieShop.ViewModels.Admin
{
	public class DeleteUserFromRoleViewModel
	{
		public DeleteUserFromRoleViewModel()
		{
			Users = new List<ApplicationUser>();
		}

		public string RoleId { get; set; }
		public string UserId { get; set; }
		public List<ApplicationUser> Users { get; set; }
	}
}
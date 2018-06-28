using System;
using Microsoft.AspNetCore.Identity;

namespace PieShop.Auth
{
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser() {}

		public ApplicationUser(string userName)
			: base(userName) {}

		public DateTime BirthDate { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
	}
}
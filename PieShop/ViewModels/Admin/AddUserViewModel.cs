using System;
using System.ComponentModel.DataAnnotations;

namespace PieShop.ViewModels.Admin
{
	public class AddUserViewModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email address")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Date)]
		public DateTime Birthdate { get; set; }

		public string City { get; set; }

		public string Country { get; set; }
	}
}
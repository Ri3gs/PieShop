﻿using System.ComponentModel.DataAnnotations;

namespace PieShop.ViewModels.Account
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
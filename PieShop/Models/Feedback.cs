using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PieShop.Models
{
	public class Feedback
	{
		[BindNever]
		public int Id { get; set; }

		[Required(ErrorMessage = "Your name is required")]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Your email is not correct")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Your message is required")]
		[StringLength(5000)]
		public string Message { get; set; }

		public bool ContactMe { get; set; }
	}
}
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;

namespace PieShop.Controllers
{
	public class FeedbackController : Controller
	{
		private readonly IFeedbackRepository _feedbackRepository;

		public FeedbackController(IFeedbackRepository feedbackRepository)
		{
			_feedbackRepository = feedbackRepository;
		}

		[HttpGet]
		public IActionResult Index() =>
			View();

		[HttpPost]
		public IActionResult Index([FromForm]Feedback feedback)
		{
			if (ModelState.IsValid)
			{
				_feedbackRepository.AddFeedback(feedback);
				return RedirectToAction("FeedbackComplete");
			}

			return Index();
		}

		public IActionResult FeedbackComplete()
			=> View();
	}
}

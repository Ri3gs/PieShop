using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;

namespace PieShop.Controllers
{
	public class PieDataController : Controller
	{
		private readonly IPieRepository _pieRepository;

		public PieDataController(IPieRepository pieRepository)
		{
			_pieRepository = pieRepository;
		}

		[HttpGet]
		public IEnumerable<PieViewModel> LoadMorePies()
		{
			IEnumerable<Pie> dbPies = null;

			dbPies = _pieRepository.GetAll().OrderBy(p => p.Id).Take(10);

			List<PieViewModel> pies = new List<PieViewModel>();

			foreach (var dbPie in dbPies)
			{
				pies.Add(MapDbPieToPieViewModel(dbPie));
			}
			return pies;
		}

		private PieViewModel MapDbPieToPieViewModel(Pie dbPie)
		{
			return new PieViewModel
			{
				PieId = dbPie.Id,
				Name = dbPie.Name,
				Price = dbPie.Price,
				ShortDescription = dbPie.ShortDescription,
				ImageThumbnailUrl = dbPie.ImageThumbnailUrl
			};
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PieShop.Models
{
	public class PieRepository : IPieRepository
	{
		private readonly AppDbContext _dbContext;

		public PieRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<Pie> GetPiesOfTheWeek() =>
			_dbContext.Pies
				.Include(pie => pie.Category)
				.Where(pie => pie.IsPieOfTheWeek);

		public IEnumerable<Pie> GetAll() =>
			_dbContext.Pies
				.Include(pie => pie.Category);

		public Pie GetById(int id) =>
			_dbContext.Pies.FirstOrDefault(pie => pie.Id == id);

		public void UpdatePie(Pie pie)
		{
			_dbContext.Pies.Update(pie);
			_dbContext.SaveChanges();
		}

		public void CreatePie(Pie pie)
		{
			_dbContext.Pies.Add(pie);
			_dbContext.SaveChanges();
		}
	}
}
﻿using System.Collections.Generic;

namespace PieShop.Models
{
	public interface IPieRepository
	{
		IEnumerable<Pie> GetPiesOfTheWeek();
		IEnumerable<Pie> GetAll();
		Pie GetById(int id);
		void UpdatePie(Pie pie);
		void CreatePie(Pie pie);
	}
}
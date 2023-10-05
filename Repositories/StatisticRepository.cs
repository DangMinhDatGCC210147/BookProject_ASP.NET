using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class StatisticRepository : IStatisticRepository
	{
		public StatisticDAO statisticDAO = new StatisticDAO();
		public List<MonthlyRevenueByGenre> ByGenres(DateTime currentDate)
		{
			return statisticDAO.GetRevenueByGenres(currentDate);
        }

		public decimal ByDay(DateTime currentDate)
		{			
			return statisticDAO.GetRevenueForCurrentDay(currentDate);
		}

		public decimal ByMonth(DateTime currentDate)
		{
			StatisticDAO statisticDAO = new StatisticDAO();
			return statisticDAO.GetRevenueForCurrentMonth(currentDate);
		}

		public List<Order> PerDayOfMonth(DateTime currentDate)
		{
			throw new NotImplementedException();
		}
	}
}

using BusinessObjects.DTO;
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
		public List<RevenueByGenre> ByGenres(DateTime currentDate)
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

        public List<RevenueByPublisher> ByPublisher(DateTime currentDate)
        {
            StatisticDAO statisticDAO = new StatisticDAO();
            return statisticDAO.GetRevenueByPublisher(currentDate);
        }

        public List<DailyRevenue> PerDayOfMonth(DateTime currentDate)
		{
			StatisticDAO statisticDAO = new StatisticDAO();
			return statisticDAO.GetPerDayOfMonth(currentDate);
		}

        public List<RevenueBestSelling> BestSelling(DateTime currentDate)
		{
			StatisticDAO statisticDAO = new StatisticDAO();
			return statisticDAO.GetBestSelling(currentDate);
		}
	}
}

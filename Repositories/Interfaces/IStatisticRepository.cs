using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IStatisticRepository
	{
		decimal ByDay(DateTime currentDate);
		decimal ByMonth(DateTime currentDate);
		List<DailyRevenue> PerDayOfMonth(DateTime currentDate);
        List<RevenueByGenre> ByGenres(DateTime currentDate);
        List<RevenueByPublisher> ByPublisher(DateTime currentDate);
        List<RevenueBestSelling> BestSelling(DateTime currentDate);
	}
}

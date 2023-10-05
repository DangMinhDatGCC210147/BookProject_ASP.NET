
using BusinessObjects;
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
		List<Order> PerDayOfMonth(DateTime currentDate);
        List<MonthlyRevenueByGenre> ByGenres(DateTime currentDate);
	}
}

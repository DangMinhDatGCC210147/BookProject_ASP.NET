using BusinessObjects;

namespace BookStoreAPI.Models
{
	public class StatisticView
	{
		public decimal RevenueByDay { get; set; }
		public decimal RevenueByMonth { get; set; }
		public MonthlyRevenueByGenre[] RevenueByGenre { get; set; }
        public decimal PerDayOfMonth { get; set; }
    }
}

using BusinessObjects.DTO;

namespace BookStoreAPI.Models
{
    public class StatisticView
	{
		public decimal RevenueByDay { get; set; }
		public decimal RevenueByMonth { get; set; }
		public List<DailyRevenue> PerDayOfMonth { get; set; }
		public List<RevenueByGenre> RevenueByGenre { get; set; }
		public List<RevenueByPublisher> RevenueByPublisher { get; set; }	
		public List<RevenueBestSelling> BestSelling { get; set; }
    }
}

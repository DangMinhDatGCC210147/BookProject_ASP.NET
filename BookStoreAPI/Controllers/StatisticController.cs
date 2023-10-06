using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using BusinessObjects;
using BookStoreAPI.Models;

namespace BookStoreAPI.Controllers
{
    [Route("api/Statistics")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticRepository repository = new StatisticRepository();

        [HttpGet]
        public ActionResult<StatisticView> GetSatistics(DateTime currentDate)
        {
            StatisticView statistic = new StatisticView();
            statistic.RevenueByMonth = repository.ByMonth(currentDate);
            statistic.RevenueByDay = repository.ByDay(currentDate);
			statistic.PerDayOfMonth = repository.PerDayOfMonth(currentDate);
			statistic.RevenueByGenre = repository.ByGenres(currentDate);
			statistic.RevenueByPublisher = repository.ByPublisher(currentDate);
			statistic.BestSelling = repository.BestSelling(currentDate);
			return statistic;
        }
    }
}

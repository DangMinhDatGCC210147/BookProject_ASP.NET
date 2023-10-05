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
            string[][] genre;
            StatisticView statistic = new StatisticView();
            statistic.RevenueByMonth = repository.ByMonth(currentDate);
            statistic.RevenueByDay = repository.ByDay(currentDate);
            //List<MonthlyRevenueByGenre> revenueByGenre = repository.ByGenres(currentDate);
            //foreach (var item in revenueByGenre)
            //{
            //    Console.WriteLine("item: ");
            //    Console.WriteLine(item.GenreName);
            //    Console.WriteLine(item.TotalRevenue);
            //    Console.WriteLine(item.ByDay);
            //}
            return statistic;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class MonthlyRevenueByGenre
    {
        public string GenreName { get; set; }
        public decimal TotalRevenue { get; set; }
        public string ByDay { get; set; }
    }
}

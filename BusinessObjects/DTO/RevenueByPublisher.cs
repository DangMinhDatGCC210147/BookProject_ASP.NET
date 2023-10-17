using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class RevenueByPublisher
    {
        public string Publisher { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}

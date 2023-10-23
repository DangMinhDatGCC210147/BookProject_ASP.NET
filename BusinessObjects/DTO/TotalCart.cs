using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class TotalCart
    {
        public decimal SubTotal { get; set; }
        public int Discount { get; set; }
        public decimal Total { get; set; }
        public int DiscountId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class UserPayment
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string SubAddress { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Total { get; set; }
        public int DiscountId { get; set; }
	}
}

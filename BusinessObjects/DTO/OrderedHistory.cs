using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
	public class OrderedHistory
	{
		public Order Order { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
	}
}

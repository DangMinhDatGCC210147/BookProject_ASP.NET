using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
	public class RevenueBestSelling
	{
		public string BookName { get; set; }
		public string Image { get; set; }
		public decimal Price { get; set; }
		public int Sold { get; set; }
		public decimal Revenue { get; set; }
	}
}

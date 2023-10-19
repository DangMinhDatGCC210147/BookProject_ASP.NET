using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
	public class ShopView
	{
		public Filter Filter { get; set; }
		public List<BookList> Books { get; set; }
	}
}

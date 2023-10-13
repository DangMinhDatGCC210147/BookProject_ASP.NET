using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class BookCart
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }  
		public int Quantity { get; set; }
		public decimal SubTotal { get; set; }
	}
}

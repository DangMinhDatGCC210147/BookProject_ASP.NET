using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class BookFavourite
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal SellingPrice { get; set; }  
		public bool Status { get; set; }
	}
}

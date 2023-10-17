using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class BookList
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal SellingPrice { get; set; }  
        public decimal Quantity { get; set; }  
        public double Rate { get; set; }  
        public int IsFavorite { get; set; }
	}
}

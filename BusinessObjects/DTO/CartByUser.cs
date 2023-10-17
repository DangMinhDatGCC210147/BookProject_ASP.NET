using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class CartQuantity
    {
        public int bookId { get; set; }
        public int newQuantity { get; set; }
        public string userId { get; set; }
    }
}

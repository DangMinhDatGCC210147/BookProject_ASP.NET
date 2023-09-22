using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
    }
}

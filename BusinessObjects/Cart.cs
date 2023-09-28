using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}

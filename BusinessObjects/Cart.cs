using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
		public ICollection<CartDetail>? CartDetails { get; set;}

	}
}

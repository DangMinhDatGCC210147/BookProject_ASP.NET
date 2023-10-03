using BusinessObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
		[ForeignKey("UserId")]
		public virtual AppUser? User { get; set; }
		public virtual ICollection<CartDetail>? CartDetails { get; set;}

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusinessObjects
{
	public class Order
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public DateTime DeliveryDate { get; set; }
		[Required]
		public string DeleveryLocal { get; set; }
		[Required]
		public string UserId { get; set; }
		[Required]
		public string CustomerName { get; set; }
		[Required]
		public string CustomerPhone { get; set; }
		[Required]
		public decimal Total { get; set; }
		[Required]
		public Boolean IsConfirm { get; set; }
		[Required]
		public int DiscountId { get; set; }
		[ForeignKey("DiscountId")]
		public virtual Discount Discount { get; set; }
		[ForeignKey("UserId")]
		public virtual AppUser? User { get; set; }
		public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
	}
}

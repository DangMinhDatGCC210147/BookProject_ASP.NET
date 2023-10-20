using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;

namespace BusinessObjects
{
	public class CartDetail
	{
		[Key]
		[Column(Order = 1)]
		public int BookId { get; set; }
		[Key]
		[Column(Order = 2)]
		public int CartId { get; set; }
		[Required]
		public int Quantity { get; set; }
		[ForeignKey("BookId")]
		public virtual Book? Book { get; set; }
		[ForeignKey("CartId")]
        public virtual Cart? Cart { get; set; }
    }
}

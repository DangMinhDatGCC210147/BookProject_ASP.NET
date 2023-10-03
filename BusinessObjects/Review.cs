using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
	public class Review
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Comment { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public int Rate { get; set; }
		[Required]
		public string UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual AppUser? User { get; set; }
		[Required]
		public int BookId { get; set; }
		[ForeignKey("BookId")]
		public virtual Book? Book { get; set; }
	}
}

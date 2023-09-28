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
		public int ID { get; set; }
		[Required]
		public int BookId { get; set; }
		public Book Book { get; set; }
		[Required]
		public string Comment { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[Required]
		public int Rate { get; set; }
	}
}

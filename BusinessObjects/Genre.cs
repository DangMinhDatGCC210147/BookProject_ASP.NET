using BusinessObjects.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
	public class Genre
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
        public DateTime AddDate { get; set; } = DateTime.Now;
        [Required]
        public GenerApproval ApprovalStatus { get; set; }
		public ICollection<Book>? Books { get; set;}
	}
}

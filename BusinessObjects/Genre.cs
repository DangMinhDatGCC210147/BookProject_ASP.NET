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
<<<<<<< HEAD
		public GenerApproval ApprovalStatus { get; set; }
		public virtual ICollection<Book>? Books { get; set;}
    }
=======
        public DateTime AddDate { get; set; }
        [Required]
        public GenerApproval ApprovalStatus { get; set; }
		public ICollection<Book>? Books { get; set;}
	}
>>>>>>> 4fb6536b0732d8aad3d143ceda8aa502a64e70f0
}

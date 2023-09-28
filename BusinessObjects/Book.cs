using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
	public class Book
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Image { get; set; }
		[Required]
		public decimal OriginalPrice { get; set; }
		[Required]
		public decimal SellingPrice { get; set; }
		[Required]
		public string ISBN { get; set; }
		[Required]
		public int PageCount { get; set; }
		[Required]
		public bool IsStatus { get; set; }
		[Required]
		public int PublicationYear { get; set; }
		[Required]
		public int LanguageId { get; set; }
		public Language Language { get; set; }
		[Required]
		public int AuthorId { get; set; }
		public Author Author { get; set; }
		[Required]
		public int GenreId { get; set; }
		public Genre Genre { get; set; }
		public ICollection<Book> Books { get; set;}
	}
}
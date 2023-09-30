using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
	public class Book
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
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
		public bool IsSale { get; set; }
		[Required]
		public int PublicationYear { get; set; }
		[Required]
		public int LanguageId { get; set; }
		[ForeignKey("LanguageId")]
		public virtual Language? Language { get; set; }
		[Required]
		public virtual int AuthorId { get; set; }
		[ForeignKey("AuthorId")]
		public virtual Author? Author { get; set; }
		[Required]
		public int GenreId { get; set; }
		[ForeignKey("GenreId")]
		public virtual Genre? Genre { get; set; }

		public ICollection<Favourite>? Favourites { get; set;}
		public ICollection<OrderDetail>? OrderDetails { get; set;}
		public ICollection<Review>? Reviews { get; set; }

	}
}
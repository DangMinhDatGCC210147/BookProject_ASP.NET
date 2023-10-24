using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
	public class BookGenre
	{
		public int BookId { get; set; }
		public string BookTitle { get; set; }
		public string BookImage { get; set; }
		public string AuthorName { get; set; }
		public double ReviewRate { get; set; }
		public bool IsFavourite { get; set; }
	}

	public class TopGenre
	{
		public int GenreId { get; set; }
		public string GenreName { get; set; }
		public List<BookGenre> BookGenres { get; set; }
		public decimal TotalSold { get; set; }
	}
}

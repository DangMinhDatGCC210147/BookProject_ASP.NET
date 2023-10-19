using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
	public class BookHome
	{
		public List<BookAuthor> topAuthors {  get; set; }
		public List<BookGenre> topGenres {  get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
	public class BookHome
	{
		public List<TopAuthor> TopAuthors {  get; set; }
		public List<TopGenre> TopGenres {  get; set; }
	}
}

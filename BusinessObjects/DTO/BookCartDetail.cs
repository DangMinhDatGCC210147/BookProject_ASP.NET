using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class BookCartDetail
    {
        public List<BookCart> BookCarts {  get; set; }
        public TotalCart TotalCart { get; set; }
	}
}

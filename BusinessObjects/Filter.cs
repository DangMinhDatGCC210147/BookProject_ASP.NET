using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Filter
    {
        public List<GetNameAndQuantity> genres {  get; set; }
        public List<GetNameAndQuantity> publishers {  get; set; }
        public List<GetNameAndQuantity> languages {  get; set; }
        public List<GetNameAndQuantity> authors {  get; set; }

    }
}

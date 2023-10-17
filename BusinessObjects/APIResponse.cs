using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class APIResponse
    {
        public int ResponseCode { get; set; }
        public string Error { get; set; }
        public string Result { get; set; }
        public string Errormessage { get; set; }
    }
}

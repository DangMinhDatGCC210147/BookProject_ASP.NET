using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class UserProfile
    {
        public AppUser User { get; set; }
        public ChangePassword ChangePassword { get; set; }
    }
}

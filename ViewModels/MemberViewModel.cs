using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class MemberViewModel
    {
        public string memberName { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string memberUserName { get; set; }
        public string memberPassword { get; set; }
        public DateTime birthday { get; set; }
        public int Gender { get; set; }
        public string IDnumber { get; set; }
    }
}

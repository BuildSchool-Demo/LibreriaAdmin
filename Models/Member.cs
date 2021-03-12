using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Member
    {
        public Member()
        {
            Favorites = new HashSet<Favorite>();
            Orders = new HashSet<Order>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MemberUserName { get; set; }
        public string MemberPassword { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
        public string Idnumber { get; set; }
        public int? RoleId { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}

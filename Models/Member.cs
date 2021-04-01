using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("member")]
    public partial class Member
    {
        public Member()
        {
            Favorites = new HashSet<Favorite>();
            Orders = new HashSet<Order>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        [Key]
        [Column("memberId")]
        public int MemberId { get; set; }
        [Required]
        [Column("memberName")]
        [StringLength(50)]
        public string MemberName { get; set; }
        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string HomeNumber { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [Column("memberUserName")]
        [StringLength(50)]
        public string MemberUserName { get; set; }
        [Required]
        [Column("memberPassword")]
        [StringLength(1024)]
        public string MemberPassword { get; set; }
        [Column("birthday", TypeName = "date")]
        public DateTime? Birthday { get; set; }
        public int Gender { get; set; }
        [Column("IDnumber")]
        [StringLength(10)]
        public string Idnumber { get; set; }
        public int? RoleId { get; set; }
        [StringLength(10)]
        public string City { get; set; }
        [StringLength(10)]
        public string Region { get; set; }
        [Column("LineUserID")]
        [StringLength(512)]
        public string LineUserId { get; set; }
        public bool? Change { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Members")]
        public virtual Role Role { get; set; }
        [InverseProperty(nameof(Favorite.Member))]
        public virtual ICollection<Favorite> Favorites { get; set; }
        [InverseProperty(nameof(Order.Member))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(ShoppingCart.Member))]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            Members = new HashSet<Member>();
        }

        [Key]
        [Column("RoleID")]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [InverseProperty(nameof(Member.Role))]
        public virtual ICollection<Member> Members { get; set; }
    }
}

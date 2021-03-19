using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class AspNetUserRole
    {
        [Key]
        [StringLength(128)]
        public string UserId { get; set; }
        [Key]
        [StringLength(128)]
        public string RoleId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("Invoice")]
    public partial class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }
        [Required]
        [StringLength(50)]
        public string TypeInfo { get; set; }
    }
}

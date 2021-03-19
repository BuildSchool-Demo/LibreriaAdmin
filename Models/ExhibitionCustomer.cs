using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("ExhibitionCustomer")]
    public partial class ExhibitionCustomer
    {
        public ExhibitionCustomer()
        {
            ExhibitionOrders = new HashSet<ExhibitionOrder>();
        }

        [Key]
        public int ExCustomerId { get; set; }
        [Required]
        [StringLength(50)]
        public string ExCustomerName { get; set; }
        [Required]
        [StringLength(50)]
        public string ExCustomerPhone { get; set; }
        [Required]
        [StringLength(50)]
        public string ExCustomerEmail { get; set; }

        [InverseProperty(nameof(ExhibitionOrder.ExCustomer))]
        public virtual ICollection<ExhibitionOrder> ExhibitionOrders { get; set; }
    }
}

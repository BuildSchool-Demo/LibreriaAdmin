using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    [Table("ExhibitionOrder")]
    public partial class ExhibitionOrder
    {
        [Key]
        public int ExOrderId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int ExCustomerId { get; set; }
        public string PaymentState { get; set; }
        [Column("customerVerify")]
        public bool CustomerVerify { get; set; }

        [ForeignKey(nameof(ExCustomerId))]
        [InverseProperty(nameof(ExhibitionCustomer.ExhibitionOrders))]
        public virtual ExhibitionCustomer ExCustomer { get; set; }
    }
}

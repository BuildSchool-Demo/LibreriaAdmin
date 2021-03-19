using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int OrderId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ShippingDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; }
        [Column("memberId")]
        public int MemberId { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipName { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipCity { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipRegion { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipAddress { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipPostalCode { get; set; }
        public int InvoiceType { get; set; }
        [StringLength(50)]
        public string InvoiceInfo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
        public int PaymentType { get; set; }
        public string PaymentState { get; set; }

        [ForeignKey(nameof(MemberId))]
        [InverseProperty("Orders")]
        public virtual Member Member { get; set; }
        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

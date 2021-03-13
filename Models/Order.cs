using System;
using System.Collections.Generic;

#nullable disable

namespace LibreriaAdmin.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int MemberId { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipAddress { get; set; }
        public string ShipPostalCode { get; set; }
        public int InvoiceType { get; set; }
        public string InvoiceInfo { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int PaymentType { get; set; }
        public string PaymentState { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

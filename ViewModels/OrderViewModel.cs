using LibreriaAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class OrderViewModel
    {
        public class OrderBaseModle
        {


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
        public class OrderListResult
        {
            public List<OrderSingleResult> OrderList { get; set; }
        }
        public class OrderSingleResult:OrderBaseModle
        {

        }
        public class OrderDateResult
        {
            public DateTime OrderDate { get; set; }
        }
    }
    
}

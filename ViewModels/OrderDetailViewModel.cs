using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class OrderDetailViewModel
    {
        public class OrderBaseModle
        {
            /// <summary>
            /// 訂單細項編號
            /// </summary>
            public int OrderDetailId { get; set; }

            /// <summary>
            /// 訂單編號
            /// </summary>
            public int OrderId { get; set; }

            /// <summary>
            /// 數量
            /// </summary>
            public int Quantity { get; set; }

            /// <summary>
            /// 產品編號
            /// </summary>
            public int ProductId { get; set; }

            /// <summary>
            /// 書名
            /// </summary>
            public string ProductName { get; set; }

            /// <summary>
            /// 單價
            /// </summary>
            public decimal UnitPrice { get; set; }

            /// <summary>
            /// 特價
            /// </summary>
            public decimal SpecialPrice { get; set; }

            /// <summary>
            /// 本項金額
            /// 此屬性唯讀
            /// </summary>
            public decimal DetailPrice { get {
                    if (SpecialPrice > 0) { return SpecialPrice * Quantity; }
                    return UnitPrice * Quantity;
            } }
        }

        public class OrderListResult
        {
            public List<OrderDetailViewModel.OrderSingleResult> OrderDetailList { get; set; }
        }
        public class OrderSingleResult : OrderBaseModle
        {

        }
        public class OrderDateResult
        {
            public DateTime OrderDate { get; set; }
        }


    }
}

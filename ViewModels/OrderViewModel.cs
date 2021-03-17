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
            /// <summary>
            /// 訂單編號
            /// </summary>
            public int OrderId { get; set; }

            /// <summary>
            /// 出貨時間
            /// </summary>
            public DateTime ShippingDate { get; set; }

            /// <summary>
            /// 訂單時間
            /// </summary>
            public DateTime OrderDate { get; set; }

            /// <summary>
            /// 會員Id
            /// </summary>
            public int MemberId { get; set; }

            /// <summary>
            /// 會員姓名
            /// </summary>
            public string MemberUserName { get; set; }

            /// <summary>
            /// 收件人姓名
            /// </summary>
            public string ShipName { get; set; }

            /// <summary>
            /// 收件人縣市
            /// </summary>
            public string ShipCity { get; set; }

            /// <summary>
            /// 收件人轄區
            /// </summary>
            public string ShipRegion { get; set; }

            /// <summary>
            /// 收件人地址
            /// </summary>
            public string ShipAddress { get; set; }

            /// <summary>
            /// 收件人郵遞區號
            /// </summary>
            public string ShipPostalCode { get; set; }

            /// <summary>
            /// 發票類型
            /// </summary>
            public int InvoiceType { get; set; }

            /// <summary>
            /// 發票資訊
            /// </summary>
            public string InvoiceInfo { get; set; }

            /// <summary>
            /// 建立時間
            /// </summary>
            public DateTime CreateTime { get; set; }

            /// <summary>
            /// 更新時間
            /// </summary>
            public DateTime? UpdateTime { get; set; }

            /// <summary>
            /// 付款方式
            /// </summary>
            public int PaymentType { get; set; }

            /// <summary>
            /// 付款狀態
            /// </summary>
            public string PaymentState { get; set; }

            public List<OrderDetail> OrderDetails { get; set; }
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

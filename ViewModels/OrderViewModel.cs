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
            /// 訂單時間顯示
            /// </summary>
            public string OrderDateDisplay
            {
                get
                {
                    return $"{OrderDate.Year.ToString("D4")}-{OrderDate.Month.ToString("D2")}-{OrderDate.Day.ToString("D2")}";
                }
                set
                {
                    OrderDate = DateTime.ParseExact(value, "yyyy-mm-dd", null);
                }
            }

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
            /// 發票開立方式
            /// 1為二聯式電子發票(存入會員帳號)
            /// 2為二聯式電子發票(手機條碼載具) 
            /// 3為二聯式電子發票(自然人憑證載具)
            /// 4為二聯式電子發票(紙本證明聯)
            /// 5為三聯式電子發票
            /// 6為發票捐贈
            /// </summary>
            public int InvoiceType { get; set; }

            /// <summary>
            /// 發票開立方式顯示
            /// </summary>
            public string InvoiceTypeDisplay
            {
                get
                {
                    if (InvoiceType == 1) return "二聯式電子發票(存入會員帳號)";
                    else if (InvoiceType == 2) return "二聯式電子發票(手機條碼載具)";
                    else if (InvoiceType == 3) return "二聯式電子發票(自然人憑證載具)";
                    else if (InvoiceType == 4) return "二聯式電子發票(紙本證明聯)";
                    else if (InvoiceType == 5) return "三聯式電子發票";
                    else if (InvoiceType == 6) return "發票捐贈";
                    else return "未知";
                }
                set
                {
                    OrderBaseModle orderBaseModle = new OrderBaseModle();
                    for(orderBaseModle.InvoiceType = 1; //初始值
                        orderBaseModle.InvoiceTypeDisplay != "未知"; //條件
                        orderBaseModle.InvoiceType++ //疊代
                        )
                    {
                        if(value == orderBaseModle.InvoiceTypeDisplay)
                        {
                            InvoiceType = orderBaseModle.InvoiceType;
                            break;
                        }
                    }
                    if (InvoiceTypeDisplay == "未知") InvoiceType = 0;
                }
            }

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
            /// 1為取貨付款 2為ATM 3為信用卡
            /// </summary>
            public int PaymentType { get; set; }

            /// <summary>
            /// 付款方式顯示
            /// </summary>
            public string PaymentTypeDisplay
            {
                get
                {
                    if (PaymentType == 1) { return "取貨付款"; }
                    else if (PaymentType == 2) { return "ATM"; }
                    else if (PaymentType == 3) { return "信用卡"; }
                    return "未知";
                }
                set
                {
                    if (value == "取貨付款") PaymentType = 1;
                    else if (value == "ATM") PaymentType = 2;
                    else if (value == "信用卡") PaymentType = 3;
                    else PaymentType = 0;
                }
            }

            /// <summary>
            /// 付款狀態
            /// </summary>
            public string PaymentState { get; set; }

            /// <summary>
            /// 訂單金額
            /// 此欄位唯讀
            /// </summary>
            public decimal OrderPrice
            {
                get
                {
                    decimal TotolPrice = 0;
                    foreach (var OrderDetail in OrderDetailList)
                    {
                        TotolPrice += OrderDetail.DetailPrice;
                    }
                    return TotolPrice;
                }
            }

            public List<OrderDetailViewModel.OrderSingleResult> OrderDetailList { get; set; }
        }
        public class OrderListResult
        {
            public List<OrderViewModel.OrderSingleResult> OrderList { get; set; }
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

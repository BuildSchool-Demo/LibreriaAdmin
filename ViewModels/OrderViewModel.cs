using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class OrderViewModel
    {

        public OrderViewModel()
        {
            OrderDetailList = new List<OrderDetailViewModel>();
        }



        /// <summary>
        /// 訂單編號
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        public string DeliveryMethod { get; set; }

        /// <summary>
        /// 訂購人姓名
        /// </summary>
        public string SubscriberName { get; set; }

        /// <summary>
        /// 訂購人行動電話
        /// </summary>
        public string SubscriberCellphone { get; set; }

        /// <summary>
        /// 訂購人室內電話
        /// </summary>
        public string SubscriberTelephone { get; set; }

        /// <summary>
        /// 訂購人縣市
        /// </summary>
        public string SubscriberAddressCitySelect { get; set; }

        /// <summary>
        /// 訂購人轄區
        /// </summary>
        public string SubscriberAddressRegionSelect { get; set; }

        /// <summary>
        /// 訂購人地址
        /// </summary>
        public string SubscriberAddress { get; set; }

        /// <summary>
        /// 訂購人郵遞區號
        /// </summary>
        public string SubscriberPostalCode { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// 收件人行動電話
        /// </summary>
        public string RecipientCellphone { get; set; }

        /// <summary>
        /// 收件人市內電話
        /// </summary>
        public string RecipientTelephone { get; set; }

        /// <summary>
        /// 收件人縣市
        /// </summary>
        public string AddressCitySelect { get; set; }

        /// <summary>
        /// 收件人轄區
        /// </summary>
        public string AddressRegionSelect { get; set; }

        /// <summary>
        /// 收件人地址
        /// </summary>
        public string RecipientAddress { get; set; }

        /// <summary>
        /// 收件人郵遞區號
        /// </summary>
        public string RecipientPostalCode { get; set; }



        /// <summary>
        /// 付款方式
        /// 1為取貨付款 2為ATM 3為信用卡
        /// </summary>
        public int PaymentMethod { get; set; }

        public string PaymentMethodText { get; set; }

        /// <summary>
        /// 發票開立方式
        /// 1為二聯式電子發票(存入會員帳號)
        /// 2為二聯式電子發票(手機條碼載具) 
        /// 3為二聯式電子發票(自然人憑證載具)
        /// 4為二聯式電子發票(紙本證明聯)
        /// 5為三聯式電子發票
        /// 6為發票捐贈
        /// </summary>
        public int Invoice { get; set; }

        /// <summary>
        /// 訂購時間
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 出貨時間
        /// </summary>
        public DateTime ShippingDate { get; set; }

        /// <summary>
        /// 書
        /// </summary>
        public List<OrderDetailViewModel> OrderDetailList { get; set; }

        /// <summary>
        /// 出貨進度
        /// "準備出貨中"
        /// "已出貨，尚未送達"
        /// "貨已送達"
        /// </summary>
        public string Progress { get; set; }

        /// <summary>
        /// 訂單總金額
        /// </summary>
        public decimal OrderPrice { get; set; }

    }
}

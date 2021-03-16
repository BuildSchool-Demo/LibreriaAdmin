using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class RentalViewModel
    {
        public int ExOrderId { get; set; }

        /// <summary>
        /// 租借開始時間
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 租借結束時間
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 場地費用總額
        /// </summary>
        public decimal Price { get; set; }

        public int ExCustomerId { get; set; }

        /// <summary>
        /// 付款狀態
        /// </summary>
        public string PaymentState { get; set; }

        /// <summary>
        /// 顧客姓名
        /// </summary>
        public string ExCustomerName { get; set; }

        /// <summary>
        /// 顧客電話
        /// </summary>
        public string ExCustomerPhone { get; set; }

        /// <summary>
        /// 顧客Email
        /// </summary>
        public string ExCustomerEmail { get; set; }

        public ExhibitonDataModel ExhibitonData { get; set; }

        public class ExhibitonDataModel
        {
            public string ExName { get; set; }
            public string ExhibitionStartTime { get; set; }
            public string ExhibitionEndTime { get; set; }
        }
    }
}

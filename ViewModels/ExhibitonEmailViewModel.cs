using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class ExhibitonEmailViewModel
    {

        /// <summary>
        /// Email基底模型
        /// </summary>
        public class EmailBaseModel
        {
            public int ExhibitionId { get; set; }
            public DateTime ExhibitionStartTime { get; set; }
            public DateTime ExhibitionEndTime { get; set; }
            public string ExhibitionIntro { get; set; }
            public string MasterUnit { get; set; }
            public decimal ExhibitionPrice { get; set; }
            public DateTime EditModifyDate { get; set; }
            public int ExCustomerId { get; set; }
            public string ExPhoto { get; set; }
            public string ExName { get; set; }
            public bool ReviewState { get; set; }
            public string ExCustomerName { get; set; }
            public string ExCustomerPhone { get; set; }
            public string ExCustomerEmail { get; set; }
        }

        /// <summary>
        /// 取得多種Email模型
        /// </summary>
        public class EmailListResult
        {
            public List<EmailSingleResult> EmailList { get; set; }
        }
        
        /// <summary>
        /// 取得單一Email模型
        /// </summary>
        public class EmailSingleResult : EmailBaseModel
        {

        }

    }
}

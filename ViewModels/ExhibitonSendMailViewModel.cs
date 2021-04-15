using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class ExhibitonSendMailViewModel
    {
        public class SendMailBaseModel
        {
            public int exhibitionId { get; set; }
            public string exCustomerEmail { get; set; }
            public string sender { get; set; }
            public string recipient { get; set; }
            public string subject { get; set; }
            public string body { get; set; }
        }
        
        /// <summary>
        /// 取得多種寄信模型
        /// </summary>
        public class SendMailListResult
        {
            List<SendMailSingleResult> SendMailList { get; set; }
        }

        /// <summary>
        /// 取得單一寄信模型
        /// </summary>
        public class SendMailSingleResult : SendMailBaseModel
        {

        }
        public class GetByCustomerEmailRequest
        {
            public string ExCustomerEmail { get; set; }
        }
    }
}

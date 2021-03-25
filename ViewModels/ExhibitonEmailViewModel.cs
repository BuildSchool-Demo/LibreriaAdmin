using Microsoft.AspNetCore.Http;
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
            public string ExhibitionStartTime { get; set; }
            public string ExhibitionEndTime { get; set; }
            public string ExhibitionIntro { get; set; }
            public string MasterUnit { get; set; }
            public decimal ExhibitionPrice { get; set; }
            public int ExCustomerId { get; set; }
            public string ExPhoto { get; set; }
            public string ExName { get; set; }
            public bool ReviewState { get; set; }
            public string ExCustomerName { get; set; }
            public string ExCustomerPhone { get; set; }
            public string ExCustomerEmail { get; set; }
            public bool CustomerVerify { get; set; }
            public RentalDateModel RentalDate { get; set; }

        }
        public class RentalDateModel
        {
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }
        public class ModifyExhibitionModel
        {
            public int ExhibitionId { get; set; }
            public string ExhibitionStartTime { get; set; }
            public string ExhibitionEndTime { get; set; }
            public string ExhibitionIntro { get; set; }
            public string MasterUnit { get; set; }
            public decimal ExhibitionPrice { get; set; }
            public int ExCustomerId { get; set; }
            public IFormFile ExPhoto { get; set; }
            public string ExName { get; set; }
            public bool ReviewState { get; set; }
            public string ExCustomerName { get; set; }
            public string ExCustomerPhone { get; set; }
            public string ExCustomerEmail { get; set; }
            public bool CustomerVerify { get; set; }
        }


        /// <summary>
        /// 取得多種Email模型
        /// </summary>
        public class EmailListResult
        {
            public List<EmailSingleResult> EmailList { get; set; }
            public List<ModifyExhibitionModel> ModifyExhibitionList { get; set; }
        }
        
        /// <summary>
        /// 取得單一Email模型
        /// </summary>
        public class EmailSingleResult : EmailBaseModel
        {

        }

    }
}

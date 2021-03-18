using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class ExhibitonViewModel
    {
        public int ExhibitionId { get; set; }

        /// <summary>
        /// 展覽開始時間
        /// </summary>
        public string ExhibitionStartTime { get; set; }

        /// <summary>
        /// 展覽結束時間
        /// </summary>
        public string ExhibitionEndTime { get; set; }

        /// <summary>
        /// 展覽簡介
        /// </summary>
        public string ExhibitionIntro { get; set; }

        /// <summary>
        /// 主辦單位
        /// </summary>
        public string MasterUnit { get; set; }

        /// <summary>
        /// 展覽門票
        /// </summary>
        public decimal ExhibitionPrice { get; set; }


        public int ExCustomerId { get; set; }

        /// <summary>
        /// 展覽圖片
        /// </summary>
        public string ExPhoto { get; set; }

        /// <summary>
        /// 展覽名稱
        /// </summary>
        public string ExName { get; set; }

        /// <summary>
        /// 審查狀態
        /// </summary>
        public bool ReviewState { get; set; }
        /// <summary>
        /// 租借送出日期
        /// </summary>
        public DateTime EditModifyDate { get; set; }

    }
}

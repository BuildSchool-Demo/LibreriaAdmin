using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class ExhibitonViewModel
    {
        /// <summary>
        /// 展覽基底模型
        /// </summary>
        public class ExhibitonBaseModel
        {
            /// <summary>
            /// 展覽編號
            /// </summary>
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

            /// <summary>
            /// 客戶編號
            /// </summary>
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
        }


        /// <summary>
        /// 取得多種展覽模型
        /// </summary>
        public class ExhibitonListResult
        {
            public List<ExhibitonSingleResult> ExhibitonList { get; set; }
        }

        /// <summary>
        /// 取得單一展覽模型
        /// </summary>
        public class ExhibitonSingleResult : ExhibitonBaseModel
        {

        }
        

    }
}

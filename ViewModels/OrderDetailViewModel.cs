using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class OrderDetailViewModel
    {
            /// <summary>
            /// 訂單細項編號
            /// </summary>
            public int OrderDetailId { get; set; }

            /// <summary>
            /// 書名
            /// </summary>
            public string ProductName { get; set; }

            /// <summary>
            /// 單價
            /// </summary>
            public decimal UnitPrice { get; set; }

            /// <summary>
            /// 數量
            /// </summary>
            public int Quantity { get; set; }

            /// <summary>
            /// 產品編號
            /// </summary>
            public int ProductId { get; set; }


            /// <summary>
            /// 本項金額
            /// </summary>
            public decimal DetailPrice { get; set; }
        



    }
}

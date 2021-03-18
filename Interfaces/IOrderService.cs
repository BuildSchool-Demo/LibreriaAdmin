using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// 取得所有訂單
        /// </summary>
        /// <returns></returns>
        OrderViewModel.OrderListResult GetAll();
        OrderViewModel.OrderListResult GetTodayOrderPrice();


    }
}

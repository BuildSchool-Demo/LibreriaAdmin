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
        BaseModel.BaseResult<List<OrderViewModel.OrderSingleResult>> GetAll();
    }
}

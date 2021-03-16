using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.WebApi
{
    public class OrderController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public BaseModel.BaseResult<OrderViewModel.OrderListResult> GetAll()
        {
            BaseModel.BaseResult<OrderViewModel.OrderListResult> result = new BaseModel.BaseResult<OrderViewModel.OrderListResult>();
                result.Body = _orderService.GetAll();

            return result;
        }
    }
}

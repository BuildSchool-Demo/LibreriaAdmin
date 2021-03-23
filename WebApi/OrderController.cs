using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
            try
            {
                result.Body = _orderService.GetAll();

                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;

                return result;
            }
        }
        public BaseModel.BaseResult<OrderViewModel.OrderListResult> Getbytoday()
        {
            BaseModel.BaseResult<OrderViewModel.OrderListResult> result = new BaseModel.BaseResult<OrderViewModel.OrderListResult>();
            result.Body = _orderService.GetTodayOrderPrice();

            try
            {
                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;

                return result;
            }
        }

        [HttpPost]
        public BaseModel.BaseResult<OrderViewModel.OrderSingleResult> Edit(OrderViewModel.OrderSingleResult orderVM)
        {
            BaseModel.BaseResult<OrderViewModel.OrderSingleResult> result = new BaseModel.BaseResult<OrderViewModel.OrderSingleResult>();
            result.Body = orderVM;

            try
            {
                result.IsSuccess = _orderService.Edit(orderVM);
                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;

                return result;
            }


        }

    }
}

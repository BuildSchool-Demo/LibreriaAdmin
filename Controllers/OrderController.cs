using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderIndex()
        {
            return View();
        }

        public BaseModel.BaseResult<List<OrderViewModel.OrderSingleResult>> GetAll()
        {
            BaseModel.BaseResult<List<OrderViewModel.OrderSingleResult>>
                result = _orderService.GetAll();

            return result;
        }
    }
}

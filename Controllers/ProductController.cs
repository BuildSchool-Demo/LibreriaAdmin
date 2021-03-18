using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult ProductIndex()
        {
            return View();
        }


        public IActionResult DailySale()
        {
            return View();
        }

    }
}

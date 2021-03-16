using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public BaseModel.BaseResult<ProductViewModels.ProductListResult> GetAll()
        {                
            var result = new BaseModel.BaseResult<ProductViewModels.ProductListResult>();

            try
            {
                result.Body = _productService.GetAll();

                return result;
            }
            catch(Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;

                return result;
            }
            

        }
        [HttpGet]
        public BaseModel.BaseResult<ProductViewModels.ProductListResult> GetTotalSale()
        {
            var result = new BaseModel.BaseResult<ProductViewModels.ProductListResult>();

            try
            {
                result.Body = _productService.GetTotalSale();

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

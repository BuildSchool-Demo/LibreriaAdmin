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
            catch (Exception ex)
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
        [HttpGet("{CategoryId}")]
        public BaseModel.BaseResult<ProductViewModels.ProductListResult> GetByCategory(int CategoryId)
        {
            var result = new BaseModel.BaseResult<ProductViewModels.ProductListResult>();

            try
            {
                result.Body = _productService.GetByCategory(CategoryId);

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
        public bool DeleteItem(ProductViewModels.GetByIdRequest request)
        {

            bool isSuccess = _productService.Remove(request.ProductId);
            return isSuccess;
        }
        [HttpPost]
        public BaseModel.BaseResult<ProductViewModels.ProductSingleResult> Edit(ProductViewModels.ProductSingleResult productVM)
        {
            BaseModel.BaseResult<ProductViewModels.ProductSingleResult> result = new BaseModel.BaseResult<ProductViewModels.ProductSingleResult>();
            result.Body = productVM;
            try
            {
                result.IsSuccess = _productService.Edit(productVM);
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
        public BaseModel.BaseResult<List<ProductViewModels.ProductSingleResult>> EditIsSpecial(List
                <ProductViewModels.ProductSingleResult> productVMList)
        {
            BaseModel.BaseResult<List<ProductViewModels.ProductSingleResult>> result = new BaseModel.BaseResult
                <List<ProductViewModels.ProductSingleResult>>();
            try
            {
                result.IsSuccess = _productService.EditIsSpecial(productVMList);
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

        public BaseModel.BaseResult<LibreriaAdmin.ViewModels.ProductViewModels.ProductSingleResult> AddItem([FromBody] ProductViewModels.ProductSingleResult product)
        {

            var result = _productService.AddProduct(product);

            return result;
        }


    }

}

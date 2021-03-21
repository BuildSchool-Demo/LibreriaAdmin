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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public BaseModel.BaseResult<CategoryViewModel.CategoryListResult> GetAll()
        {
            var result = new BaseModel.BaseResult<CategoryViewModel.CategoryListResult>();
            try
            {
                result.Body = _categoryService.GetAll();

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

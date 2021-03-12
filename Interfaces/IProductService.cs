using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// 取得所有商品
        /// </summary>
        /// <returns></returns>
        BaseModel.BaseResult<List<ProductViewModels.ProductSingleResult>> GetAll();

        /// <summary>
        /// 依类别查询商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BaseModel.BaseResult<ProductViewModels.ProductListResult> GetByCategory(ProductViewModels.GetByCategoryRequest request);

        /// <summary>
        /// 依Id查询商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BaseModel.BaseResult<ProductViewModels.ProductSingleResult> GetById(ProductViewModels.GetByIdRequest request);

    }
}

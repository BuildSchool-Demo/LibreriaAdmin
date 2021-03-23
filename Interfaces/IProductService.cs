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
        ProductViewModels.ProductListResult GetAll();

        /// <summary>
        /// 依类别查询商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ProductViewModels.ProductListResult GetByCategory(int CategoryId);

        /// <summary>
        /// 依Id查询商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ProductViewModels.ProductSingleResult GetById(ProductViewModels.GetByIdRequest request);
        /// <summary>
        /// 取得銷售前8名商品
        /// </summary>
        /// <returns></returns>
        ProductViewModels.ProductListResult GetTotalSale();
        /// <summary>
        /// 依據Id刪除商品
        /// </summary>
        /// <returns></returns>
        public bool Remove(int ProductId);
        public bool Edit(ProductViewModels.ProductSingleResult orderVM);
        /// <summary>
        /// 新增商品
        /// </summary>
        /// <returns></returns>
        //public bool Create(ProductViewModels.ProductBaseModel product);

    }
}

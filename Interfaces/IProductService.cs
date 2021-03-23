﻿using LibreriaAdmin.ViewModels;
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

        public bool Remove(int ProductId);

    }
}

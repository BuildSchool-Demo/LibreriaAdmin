using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class ProductViewModels
    {
        /// <summary>
        /// 商品基底模型
        /// </summary>
        public class ProductBaseModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public string Isbn { get; set; }
            public int SupplierId { get; set; }
            public string Author { get; set; }
            public int Inventory { get; set; }
            public int CategoryId { get; set; }
            public DateTime PublishDate { get; set; }
            public int Sort { get; set; }
            public DateTime CreateTime { get; set; }
            public DateTime? UpdateTime { get; set; }
            public string Introduction { get; set; }
            public int? TotalSales { get; set; }
            public bool IsFav { get; set; }
            public bool IsSpecial { get; set; }
            public string Supplier { get; set; }
        }

        /// <summary>
        /// 取得多种商品模型
        /// </summary>
        public class ProductListResult
        {
            public List<ProductSingleResult> ProductList { get; set; }
        }

        /// <summary>
        /// 取得单一商品模型
        /// </summary>
        public class ProductSingleResult : ProductBaseModel
        {

        }
        public class GetByCategoryRequest
        {
            public int CategoryId { get; set; }
        }

        public class GetByIdRequest
        {
            public int ProductId { get; set; }
        }

        

    }
}

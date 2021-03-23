using LibreriaAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.ViewModels
{
    public class CategoryViewModel
    { 
        public class CategoryBaseModel
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }
            public int Sort { get; set; }


        }
        public class CategoryListResult
        {
            public List<CategorySingleResult> CategoryList { get; set; }
        }

        /// <summary>
        /// 取得单一商品模型
        /// </summary>
        public class CategorySingleResult : CategoryBaseModel
        {

        }

    }
}

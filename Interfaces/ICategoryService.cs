using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// 取的全部書籍分類
        /// </summary>
        /// <returns></returns>
        CategoryViewModel.CategoryListResult GetAll();
    }
}

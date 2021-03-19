using LibreriaAdmin.Interfaces;
using LibreriaAdmin.Models;
using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _dbrepository;
        public CategoryService(IRepository repository)
        {
            _dbrepository = repository;
        }
        public CategoryViewModel.CategoryListResult GetAll()
        {
            var result = new CategoryViewModel.CategoryListResult();
            result.CategoryList = _dbrepository.GetAll<Category>() 
                .Select(x => new CategoryViewModel.CategorySingleResult()
                {
                    CategoryId = x.CategoryId,
                    Name=x.Name,
                    Sort=x.Sort
                }).ToList();
            return result;
        }
    }
}

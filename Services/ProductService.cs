using LibreriaAdmin.Interfaces;
using LibreriaAdmin.Models;
using LibreriaAdmin.Repository;
using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Services
{
    public class ProductService : IProductService
    {
        private readonly LibreriaRepository _dbRepository;

        public ProductService()
        {
            _dbRepository = new LibreriaRepository();
        }

        public BaseModel.BaseResult<List<ProductViewModels.ProductSingleResult>> GetAll()
        {
            var result = new BaseModel.BaseResult<List<ProductViewModels.ProductSingleResult>>();

            result.Body = _dbRepository.GetAll<Product>()
                .Select(x => new ProductViewModels.ProductSingleResult()
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                Isbn = x.Isbn,
                SupplierId = x.SupplierId,
                Author = x.Author,
                Inventory = x.Inventory,
                CategoryId = x.CategoryId,
                PublishDate = x.PublishDate,
                Sort = x.Sort,
                CreateTime = x.CreateTime,
                UpdateTime = x.UpdateTime,
                Introduction = x.Introduction,
                TotalSales = x.TotalSales,
                IsFav = x.IsFav,
                IsSpecial = x.IsSpecial
            }).ToList();

            return result;
        }

        public BaseModel.BaseResult<ProductViewModels.ProductListResult> GetByCategory(ProductViewModels.GetByCategoryRequest request)
        {
            BaseModel.BaseResult<ProductViewModels.ProductListResult> result = new BaseModel.BaseResult<ProductViewModels.ProductListResult>();

            var productList = _dbRepository.GetAll<Product>()
                .Where(x => x.CategoryId == request.CategoryId)
                .Select(x => new ProductViewModels.ProductSingleResult()
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                Isbn = x.Isbn,
                SupplierId = x.SupplierId,
                Author = x.Author,
                Inventory = x.Inventory,
                CategoryId = x.CategoryId,
                PublishDate = x.PublishDate,
                Sort = x.Sort,
                CreateTime = x.CreateTime,
                UpdateTime = x.UpdateTime,
                Introduction = x.Introduction,
                TotalSales = x.TotalSales,
                IsFav = x.IsFav,
                IsSpecial = x.IsSpecial
            }).ToList();

            result.Body = new ProductViewModels.ProductListResult()
            {
                ProductList = productList
            };

            return result;

        }

        public BaseModel.BaseResult<ProductViewModels.ProductSingleResult> GetById(ProductViewModels.GetByIdRequest request)
        {
            var result = new BaseModel.BaseResult<ProductViewModels.ProductSingleResult>();

            result.Body = _dbRepository.GetAll<Product>()
                .Where(x => x.ProductId == request.ProductId)
                .Select(x => new ProductViewModels.ProductSingleResult()
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    Isbn = x.Isbn,
                    SupplierId = x.SupplierId,
                    Author = x.Author,
                    Inventory = x.Inventory,
                    CategoryId = x.CategoryId,
                    PublishDate = x.PublishDate,
                    Sort = x.Sort,
                    CreateTime = x.CreateTime,
                    UpdateTime = x.UpdateTime,
                    Introduction = x.Introduction,
                    TotalSales = x.TotalSales,
                    IsFav = x.IsFav,
                    IsSpecial = x.IsSpecial
                }).FirstOrDefault();

            return result;
        }
    }
}

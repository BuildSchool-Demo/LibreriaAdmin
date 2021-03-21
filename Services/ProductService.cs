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
        private readonly IRepository _dbRepository;

        public ProductService(IRepository repository)
        {
            _dbRepository = repository;
        }

        public ProductViewModels.ProductListResult GetAll()
        {
            var result = new ProductViewModels.ProductListResult();

            result.ProductList = (from p in _dbRepository.GetAll<Product>()
                                  join v in _dbRepository.GetAll<Preview>()
                                  on p.ProductId equals v.ProductId
                                  where v.Sort == 0
                                  join c in _dbRepository.GetAll<Category>()
                                  on p.CategoryId equals c.CategoryId
                                  join s in _dbRepository.GetAll<Supplier>()
                                  on p.SupplierId equals s.SupplierId
                                  select new ProductViewModels.ProductSingleResult()
                                  {
                                      ProductId = p.ProductId,
                                      ProductName = p.ProductName,
                                      UnitPrice = p.UnitPrice,
                                      Author = p.Author,
                                      Supplier = s.Name,
                                      PublishDate = p.PublishDate,
                                      CreateTime = p.CreateTime,
                                      Introduction = p.Introduction,
                                      Inventory = p.Inventory,
                                      TotalSales = p.TotalSales,

                                  }).ToList();

            return result;
        }
        public ProductViewModels.ProductListResult GetByCategory(int CategoryId)
        {
            var result = new ProductViewModels.ProductListResult();

            result.ProductList = (from p in _dbRepository.GetAll<Product>()
                                  .Where(x => x.CategoryId == CategoryId)
                                  join v in _dbRepository.GetAll<Preview>()
                                  on p.ProductId equals v.ProductId
                                  where v.Sort == 0
                                  join c in _dbRepository.GetAll<Category>()
                                  on p.CategoryId equals c.CategoryId
                                  join s in _dbRepository.GetAll<Supplier>()
                                  on p.SupplierId equals s.SupplierId
                                  select new ProductViewModels.ProductSingleResult()
                                  {
                                      ProductId = p.ProductId,
                                      ProductName = p.ProductName,
                                      UnitPrice = p.UnitPrice,
                                      Author = p.Author,
                                      Supplier = s.Name,
                                      PublishDate = p.PublishDate,
                                      CreateTime = p.CreateTime,
                                      Introduction = p.Introduction,
                                      Inventory = p.Inventory,
                                      TotalSales = p.TotalSales,
                                      IsSpecial = p.IsSpecial

                                  }).ToList();

            return result;
        }
        

        public ProductViewModels.ProductSingleResult GetById(ProductViewModels.GetByIdRequest request)
        {
            var data = _dbRepository.GetAll<Product>()
                .FirstOrDefault(x => x.ProductId == request.ProductId);

            var result = new ProductViewModels.ProductSingleResult()
            {
                ProductId = data.ProductId,
                ProductName = data.ProductName,
                UnitPrice = data.UnitPrice,
                Isbn = data.Isbn,
                SupplierId = data.SupplierId,
                Author = data.Author,
                Inventory = data.Inventory,
                CategoryId = data.CategoryId,
                PublishDate = data.PublishDate,
                Sort = data.Sort,
                CreateTime = data.CreateTime,
                UpdateTime = data.UpdateTime,
                Introduction = data.Introduction,
                TotalSales = data.TotalSales,
                //IsFav = data.IsFav,
                IsSpecial = data.IsSpecial
            };

            return result;
        }
        public ProductViewModels.ProductListResult GetTotalSale()
        {
            var result = new ProductViewModels.ProductListResult();

            result.ProductList = (from p in _dbRepository.GetAll<Product>().OrderByDescending(x => x.TotalSales).Take(8)
                                  join v in _dbRepository.GetAll<Preview>()
                                  on p.ProductId equals v.ProductId
                                  where v.Sort == 0
                                  join c in _dbRepository.GetAll<Category>()
                                  on p.CategoryId equals c.CategoryId
                                  join s in _dbRepository.GetAll<Supplier>()
                                  on p.SupplierId equals s.SupplierId
                                  select new ProductViewModels.ProductSingleResult()
                                  {
                                      ProductId = p.ProductId,
                                      ProductName = p.ProductName,
                                      UnitPrice = p.UnitPrice,
                                      Author = p.Author,
                                      Supplier = s.Name,
                                      PublishDate = p.PublishDate,
                                      CreateTime = p.CreateTime,
                                      Introduction = p.Introduction,
                                      Inventory = p.Inventory,
                                      TotalSales = p.TotalSales,

                                  }).ToList();

            return result;
        }


    }
}

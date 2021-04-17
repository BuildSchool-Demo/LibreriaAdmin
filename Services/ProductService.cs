using LibreriaAdmin.Interfaces;
using LibreriaAdmin.Models;
using LibreriaAdmin.Repository;
using LibreriaAdmin.Repository.Interface;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LibreriaAdmin.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _dbRepository;
        private readonly IMemoryCacheRepository _iMemoryCacheRepository;

        public ProductService(IRepository repository, IMemoryCacheRepository iMemoryCacheRepository)
        {
            _dbRepository = repository;
            _iMemoryCacheRepository = iMemoryCacheRepository;
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
                                      CategoryId = p.CategoryId,
                                      UnitPrice = p.UnitPrice,
                                      Author = p.Author,
                                      Supplier = s.Name,
                                      PublishDate = p.PublishDate,
                                      CreateTime = p.CreateTime,
                                      Introduction = p.Introduction,
                                      Inventory = p.Inventory,
                                      TotalSales = p.TotalSales,
                                      MainUrl = v.ImgUrl,
                                      SpecialPrice = p.SpecialPrice
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
                                      IsSpecial = p.IsSpecial,
                                      SpecialPrice = p.SpecialPrice

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
            //var result = new ProductViewModels.ProductListResult();
           var result = _iMemoryCacheRepository.Get<ProductViewModels.ProductListResult>("Product.GetTotalSale");
            if (result != null) return result;
            result = new ProductViewModels.ProductListResult();
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
                                      SpecialPrice = p.SpecialPrice

                                  }).ToList();

            
            _iMemoryCacheRepository.Set<ProductViewModels.ProductListResult>("Product.GetTotalSale", result);
            return result;
            
        }

        public bool Remove(int productId)
        {
            //判斷商品有沒有產生訂單再刪除
            bool notCanDelete = false;
            //訂單詳細
            notCanDelete = _dbRepository.GetAll<OrderDetail>().ToList()
                .Exists(OrderDetail => OrderDetail.ProductId == productId);
            if (notCanDelete == true) return false;
            //購物車
            notCanDelete = _dbRepository.GetAll<ShoppingCart>().ToList()
                .Exists(ShoppingCart => ShoppingCart.ProductId == productId);
            if (notCanDelete == true) return false;
            //訂單詳細
            notCanDelete = _dbRepository.GetAll<Favorite>().ToList()
                .Exists(Favorite => Favorite.ProductId == productId);
            if (notCanDelete == true) return false;
            //刪Preview
            List<Preview> previewList = _dbRepository.GetAll<Preview>().Where(x => x.ProductId == productId).ToList();
            foreach (Preview preview in previewList)
            {
                _dbRepository.Delete(preview);
            }
            Product product = _dbRepository.GetAll<Product>().FirstOrDefault(x => x.ProductId == productId);
            if (!(product is null))
            {
                _dbRepository.Delete(product);
                return true;
            }
            return false;
        }
        public bool EditIsSpecial(List<ProductViewModels.ProductSingleResult> productVMList)
        {
            foreach (var productVM in productVMList)
            {
                Product product = _dbRepository.GetAll<Product>().FirstOrDefault(product => product.ProductId == productVM.ProductId);
                product.IsSpecial = productVM.IsSpecial;
                product.SpecialPrice = productVM.SpecialPrice;
                try
                {
                    _dbRepository.Update(product);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public bool Edit(ProductViewModels.ProductSingleResult productVM)
        {
            Product product = _dbRepository.GetAll<Product>().FirstOrDefault(product => product.ProductId == productVM.ProductId);
            product.ProductName = productVM.ProductName;
            product.UnitPrice = productVM.UnitPrice;
            product.Inventory = productVM.Inventory;
            product.TotalSales = productVM.TotalSales;
            product.IsSpecial = productVM.IsSpecial;
            product.Introduction = productVM.Introduction;

            _dbRepository.Update(product);

            //圖片
            Preview preview = _dbRepository.GetAll<Preview>().FirstOrDefault(preview => preview.ProductId == productVM.ProductId && preview.Sort == 0);

            preview.ProductId = productVM.ProductId;
            preview.ImgUrl = productVM.MainUrl;
            preview.Sort = 0;

            _dbRepository.Update(preview);

            int i = 0;
            foreach (var imgUrl in productVM.PreviewUrls)
            {
                i++;
                preview = _dbRepository.GetAll<Preview>().FirstOrDefault(preview => preview.ProductId == productVM.ProductId && preview.Sort == i);

                preview.ProductId = productVM.ProductId;
                preview.ImgUrl = imgUrl;
                preview.Sort = i;
                _dbRepository.Update(preview);
            }

            return true;
        }

        public BaseModel.BaseResult<ProductViewModels.ProductSingleResult> AddProduct([FromBody] ProductViewModels.ProductSingleResult productVM)
        {
            var result = new BaseModel.BaseResult<ProductViewModels.ProductSingleResult>();
            Product newProduct = null;




            newProduct = new Product
            {
                ProductName = productVM.ProductName,
                ProductId = productVM.ProductId,
                CategoryId = productVM.CategoryId,
                Introduction = productVM.Introduction,
                SupplierId = productVM.SupplierId,
                Author = productVM.Author,
                Inventory = productVM.Inventory,
                TotalSales = productVM.TotalSales,
                IsSpecial = productVM.IsSpecial,
                UnitPrice = productVM.UnitPrice,
                Isbn = productVM.Isbn,
                CreateTime = DateTime.UtcNow.AddHours(8),
                Sort = 0
            };

            try
            {
                _dbRepository.Create<Product>(newProduct);
                if (newProduct != null)
                {
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg = ex.ToString();
            }



            //圖片
            Preview mainPreview = new Preview()
            {
                ProductId = newProduct.ProductId,
                ImgUrl = productVM.MainUrl,
                Sort = 0,
            };
            try
            {
                _dbRepository.Create(mainPreview);
                if (mainPreview != null)
                {
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Msg = ex.ToString();
            }


            int i = 0;
            foreach (string previewUrl in productVM.PreviewUrls)
            {
                i++;
                Preview preview = new Preview()
                {
                    ProductId = newProduct.ProductId,
                    ImgUrl = previewUrl,
                    Sort = i,
                };
                try
                {
                    _dbRepository.Create(preview);
                    if (preview != null)
                    {
                        result.IsSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Msg = ex.ToString();
                }
            }



            return result;

        }

    }
}

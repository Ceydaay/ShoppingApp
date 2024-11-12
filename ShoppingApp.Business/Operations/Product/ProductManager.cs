using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ShoppingApp.Business.Types;
using ShoppingApp.Data.Entities;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Product.Dtos
{
    // Ürün işlemlerini yöneten sınıf
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductEntity> _productRepository;

        // Yapıcı metot, UnitOfWork ve ürün deposunu (repository) dependency injection ile alır
        public ProductManager(IUnitOfWork unitOfWork, IRepository<ProductEntity> productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        // Yeni ürün ekler ve işlem sonucunu döner
        public async Task<ServiceMessage> AddProduct(AddProductDto product)
        {
            // Aynı ada sahip ürün var mı kontrol edilir
            var hasProduct = _productRepository.GetAll(x => x.ProductName.ToLower() == product.ProductName.ToLower()).Any();

            if (hasProduct)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu ürün zaten sistemde mevcut."

                };
            }

            var productEntity = new ProductEntity
            {
                //Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
            };

            _productRepository.Add(productEntity);

            try
            {
                await _unitOfWork.SaveChangeAsync();
               

                return new ServiceMessage
                {
                    IsSucceed = true,
                    Message = "Ürün başarıyla eklendi."
                };
            }
            catch (Exception)
            {

                throw new Exception("Ürün kaydı sırasında bir sorunla karşılaşıldı.");
            }
        }

        // Verilen ID'ye göre ürün bilgilerini döner
        public async Task<ProductDto> GetProducts(int id)
        {
            var entity = _productRepository.GetById(id);

            if (entity == null)
                throw new Exception("Ürün bulunamadı.");
            return new ProductDto
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Price = entity.Price,
                StockQuantity = entity.StockQuantity,

            };
        }


        // Tüm ürünleri listeler

        public async Task<List<ProductDto>> GetAllProducts()
        {

            var products = await _productRepository.GetAll().Select(x => new ProductDto
            {

                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                StockQuantity = x.StockQuantity,


            }).ToListAsync();

            return products;

        }

   

        public async Task<ServiceMessage> AdjustProductPrice(int id, int changeTo)
        {
            var product = _productRepository.GetById(id);

            if (product is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu id ile eşleşen ürün bulunamadı."

                };
            }

            product.Price = changeTo;

            _productRepository.Update(product);

            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {

                throw new Exception("Ürün sayısı değiştirilirken bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Ürün fiyatı başarıyla güncellendi."

            };

        }


        public async Task<ServiceMessage> DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Silinmek istenen ürün bulunamadı."
                };
            }
            _productRepository.Delete(id);

            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                throw new Exception("Silme işlemi sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }

        // Ürünü günceller
        public async Task<ServiceMessage> UpdateProduct(UpdateProductDto product)
        {
            var productEntity = _productRepository.GetById(product.Id);

            if (productEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Ürün bulunamadı."
                };
            }

            

            productEntity.ProductName = product.ProductName;
            productEntity.Price = product.Price;
            productEntity.StockQuantity = product.StockQuantity;

            _productRepository.Update(productEntity);


            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Ürün bilgileri güncellenirken bir hata ile karşılaşıldı." + ex);
            }

            return new ServiceMessage
            {

                IsSucceed = true,
            };

        }

        // Stok miktarını günceller
        public void UpdateStock(int id, int quantity)
        {
            var entity = _productRepository.GetById(id);

            if (entity.StockQuantity < quantity)
            {
                throw new Exception();
            }

            entity.StockQuantity -= quantity;
            _productRepository.Update(entity);
        }
    }
}

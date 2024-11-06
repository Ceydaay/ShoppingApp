using Microsoft.EntityFrameworkCore;
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
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductEntity> _productRepository;

        public ProductManager(IUnitOfWork unitOfWork, IRepository<ProductEntity> productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<ServiceMessage> AddProduct(AddProductDto product)
        {
            var hasProduct = _productRepository.GetAll(x => x.ProductName.ToLower() == product.ProductName.ToLower()).Any();

            if (hasProduct)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu ürün zaten sistemde mevcut."

                };
            }

            await _unitOfWork.BeginTransaction();

            var productEntity = new ProductEntity
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
            };

            _productRepository.Add(productEntity);

              try
            {
                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitTransaction(); 

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

       

        public async Task<ProductDto> GetProduct(int id)
        {
            var product = await _productRepository.GetAll(x => x.Id == id).Select(x => new ProductDto
            {

                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                StockQuantity = x.StockQuantity,


            }).FirstOrDefaultAsync();
            return product;
           
        }

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


        public Task<List<ProductDto>> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}

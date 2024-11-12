using Microsoft.EntityFrameworkCore;
using ShoppingApp.Business.Operations.Order.Dtos;
using ShoppingApp.Business.Operations.Product.Dtos;
using ShoppingApp.Business.Types;
using ShoppingApp.Data.Entities;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Order

{
    // IOrderService arayüzünü uygulayan, sipariş yönetimi iş kurallarını yöneten sınıf
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderProductEntity> _orderProductRepository;
        private readonly IProductService _productService;

        // Constructor: Gerekli bağımlılıkları alır ve ilgili alanlara atar
        public OrderManager(IUnitOfWork unitOfWork, IRepository<OrderEntity> orderRepository, IRepository<OrderProductEntity> orderProductRepository, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _productService = productService;
          
        }

        // Yeni bir sipariş ekler
        public async Task<ServiceMessage> AddOrder(AddOrderDto order)
        {
            var hasUsers = await _orderRepository.UserExistAsync(order.CustomerId);
            if (!hasUsers)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Kullanıcı bulunamadı."

                };
            }

            // Veritabanı işlemleri için transaction başlatılır
            await _unitOfWork.BeginTransaction();

            // Yeni sipariş nesnesi oluşturulur
            var orderEntity = new OrderEntity
            {
                CustomerId = order.CustomerId,
                OrderDate = DateTime.Now,
                TotalAmount = 0,
                OrderProducts = new List<OrderProductEntity>()
            };

            foreach (var product in order.Products)
            {
                var productEntity = await _productService.GetProducts(product.ProductId);
                _productService.UpdateStock(product.ProductId, product.Quantity);
                var orderProduct = new OrderProductEntity
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    CreatedDate = DateTime.Now,
                };
                orderEntity.OrderProducts.Add(orderProduct);
                orderEntity.TotalAmount += (productEntity.Price * product.Quantity);

            }
            _orderRepository.Add(orderEntity);

            try
            {
                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {

                await _unitOfWork.RollBackTransaction();
                throw new Exception("Eklenirken hata oluştu" + ex.Message, ex);


            }

            return new ServiceMessage { IsSucceed = true };

        }

        // Sipariş silme işlemi
        public async Task<ServiceMessage> DeleteOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Silinmek istenen ürün bulunamadı."
                };

            }
            _orderRepository.Delete(order);

            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception )
            {
                throw new Exception("Silme işlemi sırasında bir hata oluştu.");
            }

            return new ServiceMessage { 
                IsSucceed = true 
            };

        }

        // Belirli bir siparişi getirir
        public async Task<OrderDto> GetOrder(int id)
        {
            var order = await _orderRepository.GetAll(x => x.Id == id).Select(x => new OrderDto
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                CustomerId = x.CustomerId,
                OrderProducts = x.OrderProducts.Select(f => new AddOrderProductDto
                {
                    ProductId = f.Product.Id,
                    Quantity = f.Quantity,


                }).ToList()
            }).FirstOrDefaultAsync();
            return order;
        }

        // Tüm siparişleri getirir
        public async Task<List<OrderDto>> GetOrders()
        {
            var orders = await _orderRepository.GetAll().Select(x => new OrderDto
            {
                Id = x.Id,
                OrderDate = x.OrderDate, 
                TotalAmount = x.TotalAmount,
                CustomerId = x.CustomerId,
                OrderProducts = x.OrderProducts.Select(f => new AddOrderProductDto
                {
                    ProductId = f.Product.Id,
                    Quantity = f.Quantity,


                }).ToList()
            }).ToListAsync();
            return orders;
        }


        // Sipariş güncelleme işlemi
        public async Task<ServiceMessage> UpdateOrder(UpdateOrderDto order)
        {
         
            var orderEntity = _orderRepository.GetById(order.Id);

            if (orderEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Ürün bulunamadı."
                };
            }

            await _unitOfWork.BeginTransaction();
           orderEntity.CustomerId = order.CustomerId;
            _orderRepository.Update(orderEntity);

            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Ürün bilgileri güncellenirken bir hata oluştu.");
            }

            var orderProducts = _orderProductRepository.GetAll(x => x.OrderId == orderEntity.Id).ToList();
            foreach (var product in orderProducts)
            {
                _orderProductRepository.Delete(product, false); //HARD DELETE
            }

            foreach (var productDto in order.Products)
            {
                var orderProduct = new OrderProductEntity
                {
                    ProductId = productDto.ProductId,
                    Quantity = productDto.Quantity,
                    OrderId = orderEntity.Id,
                };

                _orderProductRepository.Add(orderProduct);
            }
            try
            {
                await _unitOfWork.SaveChangeAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Ürün bilgileri güncellenirken bir hata oluştu. İşlemler geri alınıyor." + ex);
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }
    }
}

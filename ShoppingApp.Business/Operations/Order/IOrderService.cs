using ShoppingApp.Business.Operations.Order.Dtos;
using ShoppingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Order
{
    // Sipariş işlemleriyle ilgili iş kurallarını tanımlayan arayüz (interface)
   
    public interface IOrderService
    {
        // Verilen ID'ye sahip siparişi getirir
        Task<OrderDto> GetOrder(int id);

        // Yeni bir sipariş ekler ve ekleme sonucunu döner
        Task<ServiceMessage> AddOrder(AddOrderDto order);

        // Tüm siparişleri getirir
        Task<List<OrderDto>> GetOrders();

        // Verilen ID'ye sahip siparişi siler ve silme sonucunu döner
        Task<ServiceMessage> DeleteOrder(int id);

        // Mevcut bir siparişi günceller ve güncelleme sonucunu döner
        Task<ServiceMessage> UpdateOrder(UpdateOrderDto order);
        
      
       
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Business.Operations.Order;
using ShoppingApp.Business.Operations.Order.Dtos;
using ShoppingApp.Data.Entities;
using ShoppingApp.WebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // Constructor: IOrderService servisi enjekte ediliyor.
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]  // GET /api/orders/{id} - Sipariş detaylarını getiren metot
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrder(id);

            if (order is null)

                return NotFound();
            else
                return Ok(order);
        }

        [HttpGet]        // GET /api/orders - Tüm siparişleri getiren metot

        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrders();

            return Ok(orders);
        }

        // Yeni bir sipariş eklemek için POST metodu

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(AddOrderRequest request)
        {
            // AddOrderRequest'ten AddOrderDto'ya veri aktarımı yapılır.
            var addOrderDto = new AddOrderDto
            {
                CustomerId = request.CustomerId,
                Products = request.Products
            };

            // Siparişi ekleme işlemi yapılır.
            var result = await _orderService.AddOrder(addOrderDto);

            // Eğer işlem başarısızsa hata mesajı döndürülür.
            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            // Başarılıysa sipariş başarıyla eklendiği mesajı döndürülür.
            else
            {
                return Ok("Sipariş başarıyla eklendi.");
            }
        }


        // Siparişi silmek için DELETE metodu
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            // Sipariş silme işlemi yapılır.
            var result = await _orderService.DeleteOrder(id);

            // Eğer silme işlemi başarılıysa 200 OK döndürülür.
            if (result.IsSucceed)
            {
                return Ok();
            }
            // Eğer silme işlemi başarısızsa 400 Bad Request döndürülür.
            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)
        {
            var UpdateOrderDto = new UpdateOrderDto
            {
                Id = id,
                CustomerId = request.CustomerId,
                Products = request.Products,

            };


            var result = await _orderService.UpdateOrder(UpdateOrderDto);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return await GetOrder(id);
        }
    }
}


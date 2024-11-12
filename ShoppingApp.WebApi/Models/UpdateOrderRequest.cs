using ShoppingApp.Business.Operations.Order.Dtos;

namespace ShoppingApp.WebApi.Models
{
    public class UpdateOrderRequest
    {
        public int CustomerId { get; set; }

        public List<AddOrderProductDto> Products { get; set; }

    }
}

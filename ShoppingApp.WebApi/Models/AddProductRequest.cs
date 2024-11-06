using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebApi.Models
{
    public class AddProductRequest
    {
        [Required]
        public int Id { get; set; }  // Primary key
        [Required]
        public string ProductName { get; set; }  // Name of the product
        [Required]
        public decimal Price { get; set; }  // Price of the product
        [Required]
        public int StockQuantity { get; set; }  // Quantity of the product in stock
    }
}

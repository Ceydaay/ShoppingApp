using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebApi.Models
{
    public class UpdateProductRequest
    {
         

        [Required]
        public string ProductName { get; set; }
        public int Price { get; set; }
    
     
        public int StockQuantity { get; set; }

    }
}

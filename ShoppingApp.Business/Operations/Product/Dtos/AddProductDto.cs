using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Product.Dtos
{
    public class AddProductDto
    {
        public int Id { get; set; }  // Primary key
        public string ProductName { get; set; }  // Name of the product
        public decimal Price { get; set; }  // Price of the product
        public int StockQuantity { get; set; }  // Quantity of the product in stock
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Product.Dtos
{
    public class AddProductDto
    {
        // Ürünün adını tutan özellik
        public string ProductName { get; set; }

        // Ürünün fiyatını tutan özellik (decimal türünde para birimi için kullanılır)
        public decimal Price { get; set; }

        // Ürünün stokta bulunan miktarını tutan özellik
        public int StockQuantity { get; set; }  
    }
}

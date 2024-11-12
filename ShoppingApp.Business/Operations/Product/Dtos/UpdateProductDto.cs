using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Product.Dtos
{
    public class UpdateProductDto
    {

      public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }


        public int StockQuantity { get; set; }

    }
}

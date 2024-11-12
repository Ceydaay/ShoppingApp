using ShoppingApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Order.Dtos
{
    public class AddOrderDto
    {
     
        public int CustomerId { get; set; }
        public List<AddOrderProductDto> Products { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Business.Operations.Order.Dtos
{
    public class AddOrderProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

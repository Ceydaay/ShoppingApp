using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Entities
{
    public class OrderProductEntity : BaseEntity
    {
        public int OrderId { get; set; }  // Foreign key linking to the order
        public OrderEntity Order { get; set; }  // Navigation property for the related order

        public int ProductId { get; set; }  // Foreign key linking to the product
        public ProductEntity Product { get; set; }  // Navigation property for the related product

        public int Quantity { get; set; }  // Quantity of the product in the order

    }

    public class OrderProductConfiguration : BaseConfiguration<OrderProductEntity> { 
    
        public override void Configure (EntityTypeBuilder<OrderProductEntity> builder)
        {
            base.Configure (builder);
        }
    
    }
}

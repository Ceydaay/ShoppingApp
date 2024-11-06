using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Entities
{
    public class ProductEntity : BaseEntity
    {
        public int Id { get; set; }  // Primary key
        public string ProductName { get; set; }  // Name of the product
        public decimal Price { get; set; }  // Price of the product
        public int StockQuantity { get; set; }  // Quantity of the product in stock

    }

    public class ProductConfiguration : BaseConfiguration<ProductEntity>
    {

        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            base.Configure(builder);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Entities
{
    public class OrderEntity : BaseEntity
    {
        public int Id { get; set; }  // Primary key
        public DateTime OrderDate { get; set; }  // Date when the order was placed
        public decimal TotalAmount { get; set; }  // Total amount of the order
        public int CustomerId { get; set; }  // Foreign key linking to the customer who placed the order
        public UserEntity Customer { get; set; }  // Navigation property for the related customer
    }

    public class OrderConfiguration : BaseConfiguration<OrderEntity> {

        public override void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            base.Configure(builder);
        }

    }
}

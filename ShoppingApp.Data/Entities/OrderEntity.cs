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
        // OrderEntity, sipariş bilgilerini tutan bir sınıftır. BaseEntity'den türetilmiştir.
        public DateTime OrderDate { get; set; }  
        public decimal TotalAmount { get; set; }  
        public int CustomerId { get; set; }  
        public UserEntity Customer { get; set; }  
        public ICollection<OrderProductEntity> OrderProducts { get; set; }
    }

    // OrderEntity için yapılandırma sınıfıdır. BaseConfiguration sınıfından türetilmiştir.
    public class OrderConfiguration : BaseConfiguration<OrderEntity> {

        // Configure metodu, OrderEntity ile ilgili konfigürasyonları içerir.
        public override void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            base.Configure(builder);
        }

    }
}

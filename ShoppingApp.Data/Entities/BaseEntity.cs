using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Entities
{
    // BaseEntity sınıfı, tüm veritabanı entiteleri için ortak olan alanları tanımlar.
    public class BaseEntity
    {

        public int Id { get; set; }
        public DateTime CreatedDate {  get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

    }

    // BaseConfiguration, BaseEntity sınıfını miras alan tüm entiteler için genel yapılandırma sağlar.

    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.HasQueryFilter(x => x.IsDeleted == false);
          
            builder.Property(x => x.ModifiedDate).IsRequired(false);
        }
    }
}

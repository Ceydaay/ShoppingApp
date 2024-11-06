
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public int Id { get; set; }  // Primary key 
        public string FirstName { get; set; }  // First name of the user
        public string LastName { get; set; }  // Last name of the user
        public string Email { get; set; }  // Unique email address
        public string PhoneNumber { get; set; }  // Phone number of the user
        public string Password { get; set; }  // Password (to be encrypted using Data Protection)
        public UserType UserType { get; set; }  // Role of the user (Admin or Customer)

        // Navigation property for many-to-many relationship with orders
        public ICollection<OrderEntity> Orders { get; set; }
       
    }

    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(40);

            base.Configure(builder);
        }
    }
    
}

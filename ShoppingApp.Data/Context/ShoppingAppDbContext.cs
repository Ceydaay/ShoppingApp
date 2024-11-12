using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Data.Context
{
    // DbContext sınıfı, veritabanı ile etkileşimi sağlar. Veritabanı tabloları ve ilişkileri burada tanımlanır.
    public class ShoppingAppDbContext : DbContext
    {

        // DbContextOptions parametresi, veritabanı bağlantı bilgilerini yapılandırmak için kullanılır.

        public ShoppingAppDbContext(DbContextOptions<ShoppingAppDbContext> options) : base(options) {
        
        
        }


        // Model oluşturulurken yapılacak ayarları içerir. Fluent API kullanılarak tablo yapılandırmaları yapılır.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //fluent API

            
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenenceMode = false
                });




            base.OnModelCreating(modelBuilder);
        }

        // Veritabanındaki UserEntity tablosuna karşılık gelen DbSet
        public DbSet<UserEntity> Users => Set<UserEntity>();


        // Veritabanındaki OrderEntity tablosuna karşılık gelen DbSet
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();

        // Veritabanındaki ProductEntity tablosuna karşılık gelen DbSet
        public DbSet<ProductEntity> Products => Set<ProductEntity>();

        // Veritabanındaki OrderProductEntity tablosuna karşılık gelen DbSet
        public DbSet<OrderProductEntity> OrderProducts => Set<OrderProductEntity>();

        // Veritabanındaki SettingEntity tablosuna karşılık gelen DbSet
        public DbSet<SettingEntity> Settings {  get; set; }

    }
}

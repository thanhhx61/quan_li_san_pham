using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Productmanagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.DBContexts
{
    public class ProductShopDBContext : IdentityDbContext<AppIdentityUser>
    {
        public ProductShopDBContext(DbContextOptions<ProductShopDBContext> options) : base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedingCategory(modelBuilder);

        }

        private void SeedingCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = 1,
                    CategoryName = "Áo",
                    IsDeleted = false
                },
                new Category()
                {
                    CategoryId = 2,
                    CategoryName = "Quần",
                    IsDeleted = false
                },
                new Category()
                {
                    CategoryId = 3,
                    CategoryName = "Giầy",
                    IsDeleted = false
                });
        }


    }
    
}

using ShopApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Context
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Promotion>().ToTable("Promotion");            
        }
    }
}

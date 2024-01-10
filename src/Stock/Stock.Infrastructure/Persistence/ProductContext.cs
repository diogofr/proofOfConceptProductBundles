using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Infrastructure.Persistence
{
    
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAssociation> ProductAssociations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAssociation>()
                .HasKey(pa => new { pa.ParentProductId, pa.ChildProductId });

            modelBuilder.Entity<ProductAssociation>()
                .HasOne(pa => pa.ParentProduct)
                .WithMany(p => p.ChildrenProducts)
                .HasForeignKey(pa => pa.ParentProductId);

            modelBuilder.Entity<ProductAssociation>()
                .HasOne(pa => pa.ChildProduct)
                .WithMany()
                .HasForeignKey(pa => pa.ChildProductId);
        }
    }
}

using Danskebank_API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Danskebank_API.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration configuration;

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductSubtype> ProductSubtypes { get; set; }

        public string DbPath { get; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder add foreign keys
            builder.Entity<Product>()
                .HasOne(p => p.Type)
                .WithMany(pt => pt.Products)
                .HasForeignKey(p => p.TypeID);   

            builder.Entity<Product>().Property(p => p.ProductID).ValueGeneratedOnAdd();

            builder.Entity<Product>()
                .HasOne(p => p.Subtype)
                .WithMany(pt => pt.Products)
                .HasForeignKey(p => p.SubtypeID);
            
            builder.Entity<ProductSubtype>()
                .HasOne(s => s.Type)
                .WithMany(st => st.ProductSubtypes)
                .HasForeignKey(s => s.TypeID);

            base.OnModelCreating(builder);
        }


    }
}

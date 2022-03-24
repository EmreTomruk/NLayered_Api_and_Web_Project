using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //IEntityTypeConfiguration interface'ini uygulayan...
            //builder.ApplyConfiguration(new ProductConfiguration()); //Ustteki yapiyi kullanmazsak tek-tek tum Configuration'lari uygulamamiz gerekir...                                                                         //

            builder.Entity<ProductFeature>().HasData(
                new ProductFeature { Id = 1, Color = "Kırmızı", Height = 100, Width = 200, ProductId = 1 },
                new ProductFeature { Id = 2, Color = "Mavi", Height = 300, Width = 400, ProductId = 2 });

            base.OnModelCreating(builder);
        }
    }
}

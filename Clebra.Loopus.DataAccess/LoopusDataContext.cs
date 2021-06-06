using Microsoft.EntityFrameworkCore;

using Clebra.Loopus.Model;
using System;
using System.Collections.Generic;

namespace Clebra.Loopus.DataAccess
{
    public class LoopusDataContext : DbContext
    {
        public LoopusDataContext(DbContextOptions<LoopusDataContext> options)
            : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ClothType> ClothTypes { get; set; }
        public DbSet<LoopusUser> LoopusUsers { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<DiscountDefinition> DiscountDefinitions { get; set; }
        public DbSet<YarnType> YarnTypes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<SubImage> SubImages { get; set; }
        public DbSet<SmallText> SmallTexts { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<BigText> BigTexts { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(@"Connection String")
                    .EnableSensitiveDataLogging(false)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
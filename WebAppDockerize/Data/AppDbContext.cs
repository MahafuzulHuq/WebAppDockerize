using EFCoreCodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;
using WebAPIPrime.Models;

namespace WebAPIPrime.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)"); 
            modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<WeatherForecast>().Property(w => w.Id).ValueGeneratedOnAdd();
        }
    }
}

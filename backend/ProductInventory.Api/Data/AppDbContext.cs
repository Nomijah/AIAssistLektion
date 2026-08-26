using Microsoft.EntityFrameworkCore;
using ProductInventory.Api.Entities;

namespace ProductInventory.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var product = modelBuilder.Entity<Product>();
        product.Property(item => item.Name).HasMaxLength(120).IsRequired();
        product.Property(item => item.Description).HasMaxLength(1000);
        product.Property(item => item.InternalNotes).HasMaxLength(2000);
        product.Property(item => item.Price).HasPrecision(18, 2);
        product.Property(item => item.CostPrice).HasPrecision(18, 2);

        product.HasData(
            new Product
            {
                Id = 1,
                Name = "Mechanical Keyboard",
                Description = "Compact keyboard with tactile switches.",
                Price = 1099.00m,
                CostPrice = 620.00m,
                StockQuantity = 14,
                InternalNotes = "Demo data: reorder at five units.",
                CreatedAtUtc = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                UpdatedAtUtc = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 2,
                Name = "USB-C Hub",
                Description = "Seven-port hub for laptops.",
                Price = 649.00m,
                CostPrice = 310.00m,
                StockQuantity = 8,
                InternalNotes = "Demo data.",
                CreatedAtUtc = new DateTime(2026, 1, 2, 12, 0, 0, DateTimeKind.Utc),
                UpdatedAtUtc = new DateTime(2026, 1, 2, 12, 0, 0, DateTimeKind.Utc)
            },
            new Product
            {
                Id = 3,
                Name = "Laptop Stand",
                Description = "Adjustable aluminium stand.",
                Price = 499.00m,
                CostPrice = 205.00m,
                StockQuantity = 21,
                InternalNotes = null,
                CreatedAtUtc = new DateTime(2026, 1, 3, 12, 0, 0, DateTimeKind.Utc),
                UpdatedAtUtc = new DateTime(2026, 1, 3, 12, 0, 0, DateTimeKind.Utc)
            });
    }
}

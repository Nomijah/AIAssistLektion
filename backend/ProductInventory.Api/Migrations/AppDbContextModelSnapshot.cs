using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductInventory.Api.Data;

#nullable disable

namespace ProductInventory.Api.Migrations;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder.HasAnnotation("ProductVersion", "10.0.11");
        modelBuilder.Entity("ProductInventory.Api.Entities.Product", entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            entity.Property<decimal>("CostPrice").HasPrecision(18, 2).HasColumnType("decimal(18,2)");
            entity.Property<DateTime>("CreatedAtUtc").HasColumnType("datetime2");
            entity.Property<string>("Description").HasMaxLength(1000).HasColumnType("nvarchar(1000)");
            entity.Property<string>("InternalNotes").HasMaxLength(2000).HasColumnType("nvarchar(2000)");
            entity.Property<string>("Name").IsRequired().HasMaxLength(120).HasColumnType("nvarchar(120)");
            entity.Property<decimal>("Price").HasPrecision(18, 2).HasColumnType("decimal(18,2)");
            entity.Property<int>("StockQuantity").HasColumnType("int");
            entity.Property<DateTime>("UpdatedAtUtc").HasColumnType("datetime2");
            entity.HasKey("Id");
            entity.ToTable("Products");
            entity.HasData(
                new { Id = 1, CostPrice = 620.00m, CreatedAtUtc = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc), Description = "Compact keyboard with tactile switches.", InternalNotes = "Demo data: reorder at five units.", Name = "Mechanical Keyboard", Price = 1099.00m, StockQuantity = 14, UpdatedAtUtc = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc) },
                new { Id = 2, CostPrice = 310.00m, CreatedAtUtc = new DateTime(2026, 1, 2, 12, 0, 0, DateTimeKind.Utc), Description = "Seven-port hub for laptops.", InternalNotes = "Demo data.", Name = "USB-C Hub", Price = 649.00m, StockQuantity = 8, UpdatedAtUtc = new DateTime(2026, 1, 2, 12, 0, 0, DateTimeKind.Utc) },
                new { Id = 3, CostPrice = 205.00m, CreatedAtUtc = new DateTime(2026, 1, 3, 12, 0, 0, DateTimeKind.Utc), Description = "Adjustable aluminium stand.", Name = "Laptop Stand", Price = 499.00m, StockQuantity = 21, UpdatedAtUtc = new DateTime(2026, 1, 3, 12, 0, 0, DateTimeKind.Utc) });
        });
#pragma warning restore 612, 618
    }
}

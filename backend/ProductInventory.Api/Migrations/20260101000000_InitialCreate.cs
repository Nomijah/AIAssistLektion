using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProductInventory.Api.Data;

#nullable disable

namespace ProductInventory.Api.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260101000000_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                CostPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                StockQuantity = table.Column<int>(type: "int", nullable: false),
                InternalNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "CostPrice", "CreatedAtUtc", "Description", "InternalNotes", "Name", "Price", "StockQuantity", "UpdatedAtUtc" },
            columnTypes: new[] { "int", "decimal(18,2)", "datetime2", "nvarchar(1000)", "nvarchar(2000)", "nvarchar(120)", "decimal(18,2)", "int", "datetime2" },
            values: new object[,]
            {
                { 1, 620.00m, new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc), "Compact keyboard with tactile switches.", "Demo data: reorder at five units.", "Mechanical Keyboard", 1099.00m, 14, new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc) },
                { 2, 310.00m, new DateTime(2026, 1, 2, 12, 0, 0, DateTimeKind.Utc), "Seven-port hub for laptops.", "Demo data.", "USB-C Hub", 649.00m, 8, new DateTime(2026, 1, 2, 12, 0, 0, DateTimeKind.Utc) },
                { 3, 205.00m, new DateTime(2026, 1, 3, 12, 0, 0, DateTimeKind.Utc), "Adjustable aluminium stand.", null, "Laptop Stand", 499.00m, 21, new DateTime(2026, 1, 3, 12, 0, 0, DateTimeKind.Utc) }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Products");
    }
}

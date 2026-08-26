using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ProductInventory.Api.Data;
using ProductInventory.Api.DTOs;
using ProductInventory.Api.Services;
using Xunit;

namespace ProductInventory.Api.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task CreateAsync_SetsInternalValuesOnServer()
    {
        await using var dbContext = CreateDbContext();
        var service = new ProductService(dbContext);
        var request = new CreateProductRequest
        {
            Name = "  Monitor  ",
            Description = "  27-inch display  ",
            Price = 2499,
            StockQuantity = 4
        };

        var response = await service.CreateAsync(request, CancellationToken.None);

        var entity = await dbContext.Products.SingleAsync(product => product.Id == response.Id);
        Assert.Equal("Monitor", response.Name);
        Assert.Equal("27-inch display", response.Description);
        Assert.Equal(0, entity.CostPrice);
        Assert.Null(entity.InternalNotes);
        Assert.NotEqual(default, entity.CreatedAtUtc);
        Assert.Equal(entity.CreatedAtUtc, entity.UpdatedAtUtc);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsOnlyPublicResponseFields()
    {
        await using var dbContext = CreateDbContext();
        await dbContext.Database.EnsureCreatedAsync();
        var service = new ProductService(dbContext);

        var response = await service.GetByIdAsync(1, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Null(typeof(ProductResponse).GetProperty("CostPrice"));
        Assert.Null(typeof(ProductResponse).GetProperty("InternalNotes"));
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalseWhenProductDoesNotExist()
    {
        await using var dbContext = CreateDbContext();
        var service = new ProductService(dbContext);

        var deleted = await service.DeleteAsync(999, CancellationToken.None);

        Assert.False(deleted);
    }

    [Fact]
    public void CreateProductRequest_RejectsWhitespaceNameAndNegativeValues()
    {
        var request = new CreateProductRequest
        {
            Name = "   ",
            Price = -1,
            StockQuantity = -1
        };
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(
            request,
            new ValidationContext(request),
            validationResults,
            validateAllProperties: true);

        Assert.False(isValid);
        Assert.Equal(3, validationResults.Count);
    }

    [Fact]
    public void InitialMigration_IsDiscoverable()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=localhost;Database=MigrationCheck;TrustServerCertificate=True")
            .Options;
        using var dbContext = new AppDbContext(options);

        var migrations = dbContext.Database.GetMigrations();

        Assert.Contains("20260101000000_InitialCreate", migrations);
        Assert.False(dbContext.Database.HasPendingModelChanges());
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}

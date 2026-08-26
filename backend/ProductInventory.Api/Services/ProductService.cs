using Microsoft.EntityFrameworkCore;
using ProductInventory.Api.Data;
using ProductInventory.Api.DTOs;
using ProductInventory.Api.Entities;

namespace ProductInventory.Api.Services;

public sealed class ProductService(AppDbContext dbContext) : IProductService
{
    public async Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .AsNoTracking()
            .OrderBy(product => product.Name)
            .Select(product => ToResponse(product))
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .AsNoTracking()
            .Where(product => product.Id == id)
            .Select(product => ToResponse(product))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<ProductResponse> CreateAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var product = new Product
        {
            Name = request.Name.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description)
                ? null
                : request.Description.Trim(),
            Price = request.Price,
            CostPrice = 0,
            StockQuantity = request.StockQuantity,
            InternalNotes = null,
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ToResponse(product);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([id], cancellationToken);
        if (product is null)
        {
            return false;
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static ProductResponse ToResponse(Product product) => new(
        product.Id,
        product.Name,
        product.Description,
        product.Price,
        product.StockQuantity,
        product.CreatedAtUtc,
        product.UpdatedAtUtc);
}

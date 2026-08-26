using ProductInventory.Api.DTOs;

namespace ProductInventory.Api.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProductResponse> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

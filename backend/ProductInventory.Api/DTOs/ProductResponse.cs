namespace ProductInventory.Api.DTOs;

public sealed record ProductResponse(
    int Id,
    string Name,
    string? Description,
    decimal Price,
    int StockQuantity,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);

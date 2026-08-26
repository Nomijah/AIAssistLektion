using System.ComponentModel.DataAnnotations;

namespace ProductInventory.Api.DTOs;

public sealed class CreateProductRequest
{
    [Required]
    [StringLength(120)]
    [RegularExpression(@"^.*\S.*$", ErrorMessage = "Name must contain at least one non-whitespace character.")]
    public string Name { get; init; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; init; }

    [Range(0, 999999999.99)]
    public decimal Price { get; init; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; init; }
}

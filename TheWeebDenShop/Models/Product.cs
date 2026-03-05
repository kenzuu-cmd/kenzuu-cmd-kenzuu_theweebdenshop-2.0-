using System.ComponentModel.DataAnnotations;

namespace TheWeebDenShop.Models;

/// <summary>
/// Represents a product in the manga catalog.
/// Stored in the local SQL database via EF Core.
/// </summary>
public class Product
{
    [Key]
    [StringLength(10)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    [StringLength(500)]
    public string Short { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [StringLength(300)]
    public string Image { get; set; } = string.Empty;

    [StringLength(200)]
    public string Author { get; set; } = string.Empty;

    [StringLength(100)]
    public string Genre { get; set; } = string.Empty;

    public int Volumes { get; set; }
    public int Stock { get; set; }
    public double Rating { get; set; }
}

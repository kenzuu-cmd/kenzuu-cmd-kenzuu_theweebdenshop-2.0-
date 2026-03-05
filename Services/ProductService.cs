using Microsoft.EntityFrameworkCore;
using TheWeebDenShop.Data;
using TheWeebDenShop.Models;

namespace TheWeebDenShop.Services;

/// <summary>
/// EF Core–backed product service. Reads from the Products table in SQLite.
/// Registered as Scoped to align with DbContext lifetime.
/// </summary>
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _db;
    private static readonly Random _random = new();

    public ProductService(ApplicationDbContext db)
    {
        _db = db;
    }

    public List<Product> GetAll() =>
        _db.Products.AsNoTracking().ToList();

    public Product? GetById(string id) =>
        _db.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);

    public List<Product> GetByGenre(string genre) =>
        _db.Products.AsNoTracking()
            .Where(p => p.Genre == genre)
            .ToList();

    public List<string> GetGenres() =>
        _db.Products.AsNoTracking()
            .Select(p => p.Genre)
            .Distinct()
            .OrderBy(g => g)
            .ToList();

    public List<Product> GetFeatured(int count = 8)
    {
        // Load all then shuffle in memory (small catalog)
        var all = _db.Products.AsNoTracking().ToList();
        return all.OrderBy(_ => _random.Next()).Take(count).ToList();
    }

    public List<Product> Search(string? searchTerm, string? genre, decimal? minPrice, decimal? maxPrice)
    {
        var query = _db.Products.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.ToLower();
            query = query.Where(p =>
                p.Name.ToLower().Contains(term) ||
                p.Author.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(genre))
        {
            query = query.Where(p => p.Genre == genre);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        return query.ToList();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheWeebDenShop.Data;
using TheWeebDenShop.Models;
using TheWeebDenShop.Services;

namespace TheWeebDenShop.Pages;

/// <summary>
/// Public store view page. Shows a store's profile and its approved, non-banned manga listings.
/// Accessible to all users (anonymous or authenticated).
/// </summary>
public class StoreViewModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;

    public StoreViewModel(ApplicationDbContext db, ICartService cartService, IProductService productService)
    {
        _db = db;
        _cartService = cartService;
        _productService = productService;
    }

    public Models.Store? Store { get; set; }
    public List<Product> StoreProducts { get; set; } = new();

    public async Task OnGetAsync(string id)
    {
        Store = await _db.Stores
            .Include(s => s.Products.Where(p => p.IsApproved && !p.IsBanned))
            .FirstOrDefaultAsync(s => s.Id == id);

        if (Store != null)
        {
            StoreProducts = Store.Products.OrderByDescending(p => p.CreatedAt).ToList();
        }
    }

    /// <summary>
    /// Add to cart from the store view page.
    /// If user is not authenticated, they'll be redirected to login automatically.
    /// </summary>
    public IActionResult OnPostAddToCart(string productId)
    {
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return RedirectToPage("/Account/Login", new { returnUrl = HttpContext.Request.Path });
        }

        var product = _productService.GetById(productId);
        if (product != null && product.Stock > 0)
        {
            _cartService.AddItem(product);
            TempData["SuccessMessage"] = $"{product.Name} added to cart!";
        }

        return RedirectToPage();
    }
}

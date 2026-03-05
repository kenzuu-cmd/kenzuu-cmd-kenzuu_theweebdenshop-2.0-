using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWeebDenShop.Models;
using TheWeebDenShop.Services;

namespace TheWeebDenShop.Pages;

public class ProductsModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public ProductsModel(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }

    public List<Product> FilteredProducts { get; set; } = new();
    public List<string> Genres { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    [BindProperty(SupportsGet = true, Name = "genre")]
    public string? SelectedGenre { get; set; }

    [BindProperty(SupportsGet = true, Name = "price")]
    public string? SelectedPrice { get; set; }

    [TempData]
    public string? CartMessage { get; set; }

    public void OnGet()
    {
        Genres = _productService.GetGenres();

        decimal? minPrice = null, maxPrice = null;
        if (!string.IsNullOrEmpty(SelectedPrice))
        {
            var parts = SelectedPrice.Split('-');
            if (parts.Length == 2
                && decimal.TryParse(parts[0], out var min)
                && decimal.TryParse(parts[1], out var max))
            {
                minPrice = min;
                maxPrice = max;
            }
        }

        FilteredProducts = _productService.Search(SearchTerm, SelectedGenre, minPrice, maxPrice);
    }

    /// <summary>
    /// Handles Add to Cart POST from the product card partial.
    /// Redirects guest users to login with a friendly message.
    /// </summary>
    public IActionResult OnPostAddToCart(string productId)
    {
        // Guest users: redirect to login with return URL
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            TempData["Message"] = "Please login or register to add items to your cart.";
            return RedirectToPage("/Account/Login", new { returnUrl = Url.Page("/Products") });
        }

        var product = _productService.GetById(productId);
        if (product != null && product.Stock > 0)
        {
            _cartService.AddItem(product);
            CartMessage = $"{product.Name} added to cart!";
        }

        // Preserve current filter state on redirect
        return RedirectToPage(new { search = SearchTerm, genre = SelectedGenre, price = SelectedPrice });
    }
}

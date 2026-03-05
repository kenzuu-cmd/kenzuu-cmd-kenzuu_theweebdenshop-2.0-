using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWeebDenShop.Models;
using TheWeebDenShop.Services;

namespace TheWeebDenShop.Pages;

public class ProductDetailModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public ProductDetailModel(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }

    public Product? Product { get; set; }
    public List<Product> RelatedProducts { get; set; } = new();

    [TempData]
    public string? CartMessage { get; set; }

    public IActionResult OnGet(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return RedirectToPage("/Products");
        }

        Product = _productService.GetById(id);
        if (Product == null)
        {
            return Page(); // Show "not found" message in the view
        }

        // Get related products by genre, excluding current product
        RelatedProducts = _productService.GetByGenre(Product.Genre)
            .Where(p => p.Id != Product.Id)
            .OrderBy(_ => Random.Shared.Next())
            .Take(4)
            .ToList();

        return Page();
    }

    public IActionResult OnPostAddToCart(string productId)
    {
        // Guest users: redirect to login with a friendly message
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            TempData["Message"] = "Please login or register to add items to your cart.";
            return RedirectToPage("/Account/Login", new { returnUrl = Url.Page("/ProductDetail", new { id = productId }) });
        }

        var product = _productService.GetById(productId);
        if (product != null && product.Stock > 0)
        {
            _cartService.AddItem(product);
            CartMessage = $"{product.Name} added to cart!";
        }

        return RedirectToPage(new { id = productId });
    }
}

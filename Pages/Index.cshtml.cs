using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWeebDenShop.Models;
using TheWeebDenShop.Services;

namespace TheWeebDenShop.Pages;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly INewsletterService _newsletterService;

    public IndexModel(IProductService productService, INewsletterService newsletterService)
    {
        _productService = productService;
        _newsletterService = newsletterService;
    }

    public List<Product> FeaturedProducts { get; set; } = new();

    [TempData]
    public string? NewsletterMessage { get; set; }

    public void OnGet()
    {
        FeaturedProducts = _productService.GetFeatured(8);
    }

    /// <summary>
    /// Handles newsletter signup form POST.
    /// Named handler: asp-page-handler="Newsletter"
    /// </summary>
    public async Task<IActionResult> OnPostNewsletterAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return RedirectToPage();
        }

        await _newsletterService.SubscribeAsync(email);
        NewsletterMessage = "Thank you for subscribing! Check your email for exclusive deals.";
        return RedirectToPage();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWeebDenShop.Models;
using TheWeebDenShop.Services;

namespace TheWeebDenShop.Pages;

public class CheckoutModel : PageModel
{
    private readonly ICartService _cartService;

    public CheckoutModel(ICartService cartService)
    {
        _cartService = cartService;
    }

    [BindProperty]
    public CheckoutFormModel CheckoutForm { get; set; } = new();

    public List<CartItem> CartItems { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }

    [TempData]
    public string? SuccessMessage { get; set; }

    public IActionResult OnGet()
    {
        CartItems = _cartService.GetItems();
        if (CartItems.Count == 0)
        {
            return RedirectToPage("/Cart");
        }

        LoadOrderSummary();
        return Page();
    }

    public IActionResult OnPost()
    {
        CartItems = _cartService.GetItems();
        LoadOrderSummary();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Demo: clear cart and show success
        _cartService.Clear();
        TempData["SuccessMessage"] = "Thank you for your order! Your manga is on its way! 🎉";
        return RedirectToPage("/Index");
    }

    private void LoadOrderSummary()
    {
        CartItems = _cartService.GetItems();
        Subtotal = _cartService.GetSubtotal();
        Tax = _cartService.GetTax();
        Total = _cartService.GetTotal();
    }
}

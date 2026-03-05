using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWeebDenShop.Models;
using TheWeebDenShop.Services;

namespace TheWeebDenShop.Pages;

public class CartModel : PageModel
{
    private readonly ICartService _cartService;

    public CartModel(ICartService cartService)
    {
        _cartService = cartService;
    }

    public List<CartItem> CartItems { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }

    public void OnGet()
    {
        LoadCartData();
    }

    public IActionResult OnPostUpdateQuantity(string productId, int change)
    {
        _cartService.UpdateQuantity(productId, change);
        return RedirectToPage();
    }

    public IActionResult OnPostRemoveItem(string productId)
    {
        _cartService.RemoveItem(productId);
        return RedirectToPage();
    }

    public IActionResult OnPostClearCart()
    {
        _cartService.Clear();
        return RedirectToPage();
    }

    private void LoadCartData()
    {
        CartItems = _cartService.GetItems();
        Subtotal = _cartService.GetSubtotal();
        Tax = _cartService.GetTax();
        Total = _cartService.GetTotal();
    }
}

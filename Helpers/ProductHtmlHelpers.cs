using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheWeebDenShop.Helpers;

/// <summary>
/// HTML helpers for rendering product-related UI elements (stars, prices, stock badges).
/// Mirrors the original JS utility functions in Razor-compatible format.
/// </summary>
public static class ProductHtmlHelpers
{
    /// <summary>
    /// Formats a price in Philippine Pesos, matching the original formatPrice() JS function.
    /// </summary>
    public static string FormatPrice(decimal price)
    {
        return "₱" + price.ToString("N2");
    }

    /// <summary>
    /// Generates star rating HTML from a numeric rating (0–5),
    /// matching the original generateStars() JS function.
    /// </summary>
    public static IHtmlContent GenerateStars(IHtmlHelper html, double rating)
    {
        var stars = new System.Text.StringBuilder();
        int fullStars = (int)Math.Floor(rating);
        bool hasHalf = (rating % 1) >= 0.5;

        for (int i = 0; i < fullStars; i++)
        {
            stars.Append("<i class=\"fas fa-star text-warning\"></i>");
        }

        if (hasHalf)
        {
            stars.Append("<i class=\"fas fa-star-half-alt text-warning\"></i>");
            fullStars++;
        }

        for (int i = fullStars; i < 5; i++)
        {
            stars.Append("<i class=\"far fa-star text-warning\"></i>");
        }

        return new HtmlString(stars.ToString());
    }

    /// <summary>
    /// Returns the appropriate Bootstrap badge class for a stock level.
    /// </summary>
    public static string GetStockBadgeClass(int stock)
    {
        if (stock == 0) return "bg-danger";
        if (stock < 10) return "bg-warning";
        return "bg-success";
    }

    /// <summary>
    /// Returns display text for stock status.
    /// </summary>
    public static string GetStockText(int stock)
    {
        return stock == 0 ? "Out of Stock" : $"{stock} in stock";
    }
}

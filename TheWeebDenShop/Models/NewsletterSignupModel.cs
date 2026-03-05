using System.ComponentModel.DataAnnotations;

namespace TheWeebDenShop.Models;

/// <summary>
/// View model for newsletter signup with email validation.
/// </summary>
public class NewsletterSignupModel
{
    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;
}

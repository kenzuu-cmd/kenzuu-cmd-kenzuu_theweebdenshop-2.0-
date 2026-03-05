using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWeebDenShop.Models;

namespace TheWeebDenShop.Pages;

public class ContactModel : PageModel
{
    private readonly ILogger<ContactModel> _logger;

    public ContactModel(ILogger<ContactModel> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public ContactFormModel ContactForm { get; set; } = new();

    [TempData]
    public string? SuccessMessage { get; set; }

    public void OnGet()
    {
    }

    /// <summary>
    /// Handles contact form POST with server-side validation.
    /// Extension point: Send email via SMTP/SendGrid, or store in database.
    /// </summary>
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // TODO: Integrate with email service or save to database
        _logger.LogInformation(
            "Contact form submitted — Name: {Name}, Email: {Email}, Subject: {Subject}",
            ContactForm.Name, ContactForm.Email, ContactForm.Subject);

        SuccessMessage = "Thank you for your message! We will get back to you soon.";
        return RedirectToPage();
    }
}

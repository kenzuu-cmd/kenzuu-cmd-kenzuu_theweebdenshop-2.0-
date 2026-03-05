using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWeebDenShop.Models;

namespace TheWeebDenShop.Pages.Account;

/// <summary>
/// Admin-only login page. After successful authentication, verifies the user
/// is in the Admin role. Non-admin users are rejected with a clear message.
/// This provides a separate entry point for admin users for better UX and security.
/// </summary>
public class AdminLoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminLoginModel(SignInManager<ApplicationUser> signInManager,
                           UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    [TempData]
    public string? ErrorMessage { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "Admin email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Admin Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Attempt sign-in with lockout protection
        var result = await _signInManager.PasswordSignInAsync(
            Input.Email, Input.Password, isPersistent: false, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            // Security: Verify this user actually has Admin role
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToPage("/Admin/Dashboard");
            }

            // Not an admin — sign them out and reject
            await _signInManager.SignOutAsync();
            ModelState.AddModelError(string.Empty,
                "This account does not have admin privileges. Please use the regular login.");
            return Page();
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty,
                "Account locked due to too many failed attempts. Please try again later.");
            return Page();
        }

        ModelState.AddModelError(string.Empty, "Invalid admin credentials.");
        return Page();
    }
}

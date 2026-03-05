using Microsoft.AspNetCore.Identity;

namespace TheWeebDenShop.Models;

/// <summary>
/// Application user extending ASP.NET Core Identity.
/// Add custom profile fields here as needed.
/// </summary>
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }

    [PersonalData]
    public string? LastName { get; set; }
}

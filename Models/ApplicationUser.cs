using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TheWeebDenShop.Models;

/// <summary>
/// Application user extending ASP.NET Core Identity.
/// Each user can optionally own a Store for selling manga.
/// </summary>
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [StringLength(50)]
    public string? FirstName { get; set; }

    [PersonalData]
    [StringLength(50)]
    public string? LastName { get; set; }

    /// <summary>Navigation property to the user's store (one-to-one).</summary>
    public Store? Store { get; set; }
}

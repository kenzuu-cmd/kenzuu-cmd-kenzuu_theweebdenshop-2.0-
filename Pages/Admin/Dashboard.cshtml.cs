using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheWeebDenShop.Data;
using TheWeebDenShop.Models;

namespace TheWeebDenShop.Pages.Admin;

/// <summary>
/// Admin dashboard showing analytics and management controls
/// for all users, stores, and manga listings.
/// Protected by the "RequireAdminRole" authorization policy via convention in Program.cs.
/// </summary>
public class DashboardModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    // ── Analytics ──
    public int TotalUsers { get; set; }
    public int TotalStores { get; set; }
    public int TotalListings { get; set; }
    public int BannedListings { get; set; }

    // ── Data lists ──
    public List<ApplicationUser> Users { get; set; } = new();
    public Dictionary<string, List<string>> UserRoles { get; set; } = new();
    public Dictionary<string, Models.Store> UserStores { get; set; } = new();
    public Dictionary<string, int> StoreListingCounts { get; set; } = new();
    public List<Product> AllUserListings { get; set; } = new();

    public async Task OnGetAsync()
    {
        Users = await _userManager.Users.ToListAsync();
        TotalUsers = Users.Count;

        // Load roles for each user
        foreach (var user in Users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            UserRoles[user.Id] = roles.ToList();
        }

        // Load stores
        var stores = await _db.Stores.ToListAsync();
        TotalStores = stores.Count;
        UserStores = stores.ToDictionary(s => s.OwnerId);

        // Count listings per store
        var userListings = await _db.Products
            .Where(p => p.StoreId != null)
            .Include(p => p.Store)
            .ToListAsync();

        TotalListings = userListings.Count;
        BannedListings = userListings.Count(p => p.IsBanned);
        AllUserListings = userListings.OrderByDescending(p => p.CreatedAt).ToList();

        StoreListingCounts = userListings
            .Where(p => p.StoreId != null)
            .GroupBy(p => p.StoreId!)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    /// <summary>Admin bans a manga listing (hides it from public view).</summary>
    public async Task<IActionResult> OnPostBanListingAsync(string listingId)
    {
        var listing = await _db.Products.FindAsync(listingId);
        if (listing != null)
        {
            listing.IsBanned = true;
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = $"'{listing.Name}' has been banned.";
        }
        return RedirectToPage();
    }

    /// <summary>Admin unbans a manga listing.</summary>
    public async Task<IActionResult> OnPostUnbanListingAsync(string listingId)
    {
        var listing = await _db.Products.FindAsync(listingId);
        if (listing != null)
        {
            listing.IsBanned = false;
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = $"'{listing.Name}' has been unbanned.";
        }
        return RedirectToPage();
    }
}

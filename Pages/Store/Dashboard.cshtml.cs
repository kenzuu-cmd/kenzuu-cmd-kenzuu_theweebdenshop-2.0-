using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheWeebDenShop.Data;
using TheWeebDenShop.Models;
using TheWeebDenShop.Services;

namespace TheWeebDenShop.Pages.Store;

/// <summary>
/// Store dashboard for logged-in users. Shows their store profile and manga listings.
/// Handles store creation and manga deletion.
/// </summary>
public class DashboardModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IImageUploadService _imageService;

    public DashboardModel(ApplicationDbContext db,
                          UserManager<ApplicationUser> userManager,
                          IImageUploadService imageService)
    {
        _db = db;
        _userManager = userManager;
        _imageService = imageService;
    }

    public Models.Store? UserStore { get; set; }
    public List<Product> StoreProducts { get; set; } = new();

    // ── Store creation form ──

    [BindProperty]
    public StoreInputModel StoreInput { get; set; } = new();

    [BindProperty]
    public IFormFile? StoreLogo { get; set; }

    public class StoreInputModel
    {
        [Required(ErrorMessage = "Store name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Store name must be 3–100 characters.")]
        [Display(Name = "Store Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        [Display(Name = "Store Description")]
        public string? Description { get; set; }
    }

    public async Task OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);
        UserStore = await _db.Stores
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.OwnerId == userId);

        if (UserStore != null)
        {
            StoreProducts = UserStore.Products.OrderByDescending(p => p.CreatedAt).ToList();
        }
    }

    /// <summary>Creates a new store for the current user.</summary>
    public async Task<IActionResult> OnPostCreateStoreAsync()
    {
        var userId = _userManager.GetUserId(User);

        // Security: Prevent creating multiple stores
        var existing = await _db.Stores.AnyAsync(s => s.OwnerId == userId);
        if (existing)
        {
            TempData["ErrorMessage"] = "You already have a store.";
            return RedirectToPage();
        }

        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        string? logoPath = null;
        if (StoreLogo != null)
        {
            logoPath = await _imageService.SaveImageAsync(StoreLogo, "stores");
            if (logoPath == null)
            {
                ModelState.AddModelError("StoreLogo", "Invalid image. Use JPG, PNG, WebP, or GIF under 5 MB.");
                await OnGetAsync();
                return Page();
            }
        }

        var store = new Models.Store
        {
            OwnerId = userId!,
            Name = StoreInput.Name,
            Description = StoreInput.Description ?? string.Empty,
            LogoPath = logoPath
        };

        _db.Stores.Add(store);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = "Your store has been created! Start adding manga.";
        return RedirectToPage();
    }

    /// <summary>Deletes a manga listing owned by the current user.</summary>
    public async Task<IActionResult> OnPostDeleteMangaAsync(string mangaId)
    {
        var userId = _userManager.GetUserId(User);
        var store = await _db.Stores.FirstOrDefaultAsync(s => s.OwnerId == userId);
        if (store == null) return RedirectToPage();

        // Security: Only allow deleting manga that belongs to this user's store
        var manga = await _db.Products.FirstOrDefaultAsync(p => p.Id == mangaId && p.StoreId == store.Id);
        if (manga == null)
        {
            TempData["ErrorMessage"] = "Manga not found or access denied.";
            return RedirectToPage();
        }

        // Clean up the image file
        _imageService.DeleteImage(manga.Image);

        _db.Products.Remove(manga);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = $"'{manga.Name}' has been deleted.";
        return RedirectToPage();
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheWeebDenShop.Data;
using TheWeebDenShop.Models;
using TheWeebDenShop.Services;

namespace TheWeebDenShop.Pages.Store;

/// <summary>
/// Handles editing an existing manga listing.
/// Security: Only the store owner can edit their own manga.
/// </summary>
public class EditMangaModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IImageUploadService _imageService;

    public EditMangaModel(ApplicationDbContext db,
                          UserManager<ApplicationUser> userManager,
                          IImageUploadService imageService)
    {
        _db = db;
        _userManager = userManager;
        _imageService = imageService;
    }

    [BindProperty]
    public MangaFormModel Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var userId = _userManager.GetUserId(User);
        var store = await _db.Stores.FirstOrDefaultAsync(s => s.OwnerId == userId);
        if (store == null) return RedirectToPage("/Store/Dashboard");

        // Security: Only load manga belonging to this user's store
        var manga = await _db.Products.FirstOrDefaultAsync(p => p.Id == id && p.StoreId == store.Id);
        if (manga == null)
        {
            TempData["ErrorMessage"] = "Manga not found or access denied.";
            return RedirectToPage("/Store/Dashboard");
        }

        // Populate form
        Input = new MangaFormModel
        {
            Id = manga.Id,
            Title = manga.Name,
            Author = manga.Author,
            Description = manga.Description,
            Genre = manga.Genre,
            Price = manga.Price,
            Stock = manga.Stock,
            Volumes = manga.Volumes,
            ExistingImagePath = manga.Image
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = _userManager.GetUserId(User);
        var store = await _db.Stores.FirstOrDefaultAsync(s => s.OwnerId == userId);
        if (store == null) return RedirectToPage("/Store/Dashboard");

        var manga = await _db.Products.FirstOrDefaultAsync(p => p.Id == Input.Id && p.StoreId == store.Id);
        if (manga == null)
        {
            TempData["ErrorMessage"] = "Manga not found or access denied.";
            return RedirectToPage("/Store/Dashboard");
        }

        if (!ModelState.IsValid)
            return Page();

        // Update fields
        manga.Name = Input.Title;
        manga.Author = Input.Author;
        manga.Description = Input.Description ?? string.Empty;
        manga.Short = (Input.Description?.Length > 100
            ? Input.Description[..100] + "..."
            : Input.Description) ?? string.Empty;
        manga.Genre = Input.Genre;
        manga.Price = Input.Price;
        manga.Stock = Input.Stock;
        manga.Volumes = Input.Volumes;

        // Handle optional new cover image upload
        if (Input.CoverImage != null && Input.CoverImage.Length > 0)
        {
            var newPath = await _imageService.SaveImageAsync(Input.CoverImage, "manga");
            if (newPath == null)
            {
                ModelState.AddModelError("Input.CoverImage", "Invalid image. Use JPG, PNG, WebP, or GIF under 5 MB.");
                return Page();
            }
            // Delete old image
            _imageService.DeleteImage(manga.Image);
            manga.Image = newPath;
        }

        await _db.SaveChangesAsync();
        TempData["SuccessMessage"] = $"'{manga.Name}' has been updated.";
        return RedirectToPage("/Store/Dashboard");
    }
}

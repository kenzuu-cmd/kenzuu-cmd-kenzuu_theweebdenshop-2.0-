using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheWeebDenShop.Data;
using TheWeebDenShop.Models;
using TheWeebDenShop.Services;

namespace TheWeebDenShop.Pages.Store;

/// <summary>
/// Handles adding a new manga listing to the user's store.
/// Validates input, securely uploads cover image, and creates the Product record.
/// </summary>
public class AddMangaModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IImageUploadService _imageService;

    public AddMangaModel(ApplicationDbContext db,
                         UserManager<ApplicationUser> userManager,
                         IImageUploadService imageService)
    {
        _db = db;
        _userManager = userManager;
        _imageService = imageService;
    }

    [BindProperty]
    public MangaFormModel Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // Ensure user has a store before adding manga
        var userId = _userManager.GetUserId(User);
        var hasStore = await _db.Stores.AnyAsync(s => s.OwnerId == userId);
        if (!hasStore)
        {
            TempData["ErrorMessage"] = "Please create a store first.";
            return RedirectToPage("/Store/Dashboard");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = _userManager.GetUserId(User);
        var store = await _db.Stores.FirstOrDefaultAsync(s => s.OwnerId == userId);
        if (store == null)
        {
            TempData["ErrorMessage"] = "Please create a store first.";
            return RedirectToPage("/Store/Dashboard");
        }

        // Cover image is required for new listings
        if (Input.CoverImage == null || Input.CoverImage.Length == 0)
        {
            ModelState.AddModelError("Input.CoverImage", "A cover image is required for new listings.");
        }

        if (!ModelState.IsValid)
            return Page();

        // Securely upload the cover image
        var imagePath = await _imageService.SaveImageAsync(Input.CoverImage!, "manga");
        if (imagePath == null)
        {
            ModelState.AddModelError("Input.CoverImage", "Invalid image. Use JPG, PNG, WebP, or GIF under 5 MB.");
            return Page();
        }

        var product = new Product
        {
            Id = Guid.NewGuid().ToString(),
            Name = Input.Title,
            Author = Input.Author,
            Description = Input.Description ?? string.Empty,
            Short = Input.Description?.Length > 100
                ? Input.Description[..100] + "..."
                : Input.Description ?? string.Empty,
            Genre = Input.Genre,
            Price = Input.Price,
            Stock = Input.Stock,
            Volumes = Input.Volumes,
            Image = imagePath,
            StoreId = store.Id,
            IsApproved = true, // Auto-approved; admin can ban if needed
            Rating = 0
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = $"'{product.Name}' has been added to your store!";
        return RedirectToPage("/Store/Dashboard");
    }
}

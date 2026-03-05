# The Weeb Den Shop — ASP.NET Core 8.0

An e-commerce manga shop built with ASP.NET Core 8.0 Razor Pages, Entity Framework Core 8.0 (SQLite), and ASP.NET Core Identity.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (LTS)
- EF Core CLI tool (installed automatically below)

## Quick Start

```bash
# 1. Navigate to the project directory
cd TheWeebDenShop

# 2. Install the EF Core CLI tool (if not already installed)
dotnet tool install --global dotnet-ef

# 3. Restore NuGet packages
dotnet restore

# 4. Apply database migration (creates TheWeebDen.db with seed data)
dotnet ef database update

# 5. Run the application
dotnet run
```

The app will start at `http://localhost:5000` (or the URL shown in the terminal).

---

## Project Structure

```
TheWeebDenShop/
├── Data/
│   └── ApplicationDbContext.cs      # EF Core context + seed data
├── Helpers/
│   └── ProductHtmlHelpers.cs        # Price formatting, star ratings
├── Models/
│   ├── ApplicationUser.cs           # Identity user with FirstName/LastName
│   ├── CartItem.cs                  # Shopping cart item (session-based)
│   ├── CheckoutFormModel.cs         # Checkout form validation model
│   ├── ContactFormModel.cs          # Contact form validation model
│   ├── NewsletterSignupModel.cs     # Newsletter email model
│   └── Product.cs                   # Product entity (EF Core)
├── Pages/
│   ├── Account/
│   │   ├── AccessDenied.cshtml      # 403 page
│   │   ├── Login.cshtml             # Login page
│   │   ├── Logout.cshtml            # Logout page
│   │   └── Register.cshtml          # Registration page
│   ├── Shared/
│   │   ├── _Layout.cshtml           # Shared layout (nav, footer, auth UI)
│   │   └── _ProductCard.cshtml      # Reusable product card partial
│   ├── _ViewImports.cshtml
│   ├── _ViewStart.cshtml
│   ├── About.cshtml
│   ├── Cart.cshtml                  # [Authorize] - login required
│   ├── Checkout.cshtml              # [Authorize] - login required
│   ├── Contact.cshtml
│   ├── Error.cshtml
│   ├── Index.cshtml                 # Home page with featured products
│   ├── Privacy.cshtml
│   ├── ProductDetail.cshtml
│   ├── Products.cshtml
│   └── Terms.cshtml
├── Services/
│   ├── ICartService.cs / CartService.cs         # Session-based cart
│   ├── INewsletterService.cs / NewsletterService.cs  # Newsletter stub
│   └── IProductService.cs / ProductService.cs   # EF Core product queries
├── wwwroot/
│   ├── css/                         # common.css, theme.css
│   ├── images/                      # Product images, logo, favicon
│   └── js/                          # site.js (notifications, scroll-to-top)
├── Migrations/                      # EF Core migration files
├── appsettings.json                 # Connection string configuration
├── Program.cs                       # App startup, DI, Identity config
└── TheWeebDenShop.csproj            # .NET 8 project file
```

---

## Database

**Engine:** SQLite (file: `TheWeebDen.db` in project root)

**Connection string** (in `appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=TheWeebDen.db"
  }
}
```

### Seed Data

The database is seeded with **16 manga products** on first migration. Seed data is defined in `Data/ApplicationDbContext.cs` via `OnModelCreating`.

### Migration Commands

```bash
# Create a new migration after model changes
dotnet ef migrations add <MigrationName>

# Apply pending migrations
dotnet ef database update

# Remove the last migration (if not yet applied)
dotnet ef migrations remove

# Reset database (delete and re-migrate)
# Delete TheWeebDen.db, then run:
dotnet ef database update
```

---

## Authentication & Authorization

**Provider:** ASP.NET Core Identity with EF Core stores

### Roles
| Role     | Description                        |
|----------|------------------------------------|
| Admin    | Full access, seeded on first run   |
| Customer | Default role for new registrations |

### Default Admin Account
| Field    | Value               |
|----------|---------------------|
| Email    | admin@theweebden.com |
| Password | Admin@123!          |

> **Important:** Change the admin password after first login in a production environment.

### Password Requirements
- Minimum 8 characters
- At least 1 uppercase letter
- At least 1 lowercase letter
- At least 1 digit
- At least 1 special character
- At least 4 unique characters

### Account Lockout
- **5 failed attempts** triggers a **15-minute lockout**

### Protected Pages
| Page       | Requirement         |
|------------|---------------------|
| `/Cart`    | Authenticated user  |
| `/Checkout`| Authenticated user  |

Unauthenticated users are redirected to `/Account/Login`.

---

## NuGet Packages

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 8.0.0 | Identity with EF Core |
| Microsoft.EntityFrameworkCore.Sqlite | 8.0.0 | SQLite database provider |
| Microsoft.EntityFrameworkCore.Tools | 8.0.0 | Migration CLI support |
| Microsoft.EntityFrameworkCore.Design | 8.0.0 | Design-time EF Core tools |

---

## Development Notes

- **Cart** uses session storage (not database). Items persist for 30 minutes.
- **Newsletter** service is a stub that logs to console. Integrate with a mail provider for production.
- **Contact form** logs submissions. Integrate with SMTP/SendGrid for production email delivery.
- The `products/images/` paths in the database seed data match the original catalog. The Razor views extract the filename with `Path.GetFileName()` and prepend `~/images/`.
- Tax rate is **8%** on cart subtotal.
- Currency is **Philippine Peso (₱)**.

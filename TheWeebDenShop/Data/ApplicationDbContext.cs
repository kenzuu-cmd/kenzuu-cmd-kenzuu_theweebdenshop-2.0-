using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheWeebDenShop.Models;

namespace TheWeebDenShop.Data;

/// <summary>
/// EF Core database context with ASP.NET Core Identity integration.
/// Uses SQLite for local development.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed product data from the original catalog
        builder.Entity<Product>().HasData(
            new Product { Id = "p1", Name = "Demon Slayer Box Set (Vol.1–23)", Price = 10640m, Short = "Kimetsu no Yaiba complete manga box set", Description = "Complete 23-volume Demon Slayer manga box set with premium storage case. Follow Tanjiro's entire journey from beginning to end.", Image = "products/images/demon-slayer.jpg", Author = "Koyoharu Gotouge", Genre = "Action", Volumes = 23, Stock = 12, Rating = 4.8 },
            new Product { Id = "p2", Name = "Jujutsu Kaisen Vol. 1", Price = 503m, Short = "Yuji Itadori begins his journey", Description = "Yuji Itadori swallows Sukuna's finger and begins his chaotic days at Tokyo Jujutsu High School.", Image = "products/images/jujutsu-kaisen.jpg", Author = "Gege Akutami", Genre = "Action", Volumes = 1, Stock = 34, Rating = 4.6 },
            new Product { Id = "p3", Name = "Chainsaw Man Box Set (Vol.1–11)", Price = 4479m, Short = "Chainsaw Man Part 1 box set", Description = "Denji and Pochita's strange partnership. A bloody yet heartbreaking story collected in Part 1 box set.", Image = "products/images/chainsaw-man.jpg", Author = "Tatsuki Fujimoto", Genre = "Action", Volumes = 11, Stock = 0, Rating = 4.7 },
            new Product { Id = "p4", Name = "Attack on Titan Vol. 1", Price = 559m, Short = "Humanity's fight for survival begins", Description = "When titans breach Wall Maria, Eren Yeager witnesses unimaginable horror and vows revenge against the titans.", Image = "products/images/attack-on-titan.jpg", Author = "Hajime Isayama", Genre = "Action", Volumes = 1, Stock = 7, Rating = 4.9 },
            new Product { Id = "p5", Name = "My Hero Academia Vol. 1", Price = 503m, Short = "Izuku's journey to become a hero", Description = "Izuku Midoriya dreams of becoming a hero despite being born without a Quirk in a world where superpowers are common.", Image = "products/images/my-hero-academia.jpg", Author = "Kohei Horikoshi", Genre = "Action", Volumes = 1, Stock = 30, Rating = 4.5 },
            new Product { Id = "p6", Name = "One Piece Vol. 1", Price = 559m, Short = "Luffy begins his pirate adventure", Description = "Monkey D. Luffy sets out to sea to find the legendary treasure One Piece and become the next Pirate King.", Image = "products/images/one-piece.jpg", Author = "Eiichiro Oda", Genre = "Adventure", Volumes = 1, Stock = 40, Rating = 4.8 },
            new Product { Id = "p7", Name = "Naruto Box Set 1 (Vol.1–27)", Price = 8959m, Short = "First Naruto box set collection", Description = "Follow Naruto Uzumaki from his academy days through the Chunin Exams in this comprehensive box set.", Image = "products/images/naruto.jpg", Author = "Masashi Kishimoto", Genre = "Action", Volumes = 27, Stock = 15, Rating = 4.7 },
            new Product { Id = "p8", Name = "Tokyo Ghoul Vol. 1", Price = 503m, Short = "Ken Kaneki's transformation begins", Description = "College student Ken Kaneki's life changes forever when he encounters a ghoul and becomes a half-ghoul himself.", Image = "products/images/tokyo-ghoul.jpg", Author = "Sui Ishida", Genre = "Horror", Volumes = 1, Stock = 0, Rating = 4.4 },
            new Product { Id = "p9", Name = "Death Note Box Set", Price = 3079m, Short = "Complete Death Note collection", Description = "The complete Death Note series featuring Light Yagami's psychological battle with the detective L.", Image = "products/images/death-note.jpg", Author = "Tsugumi Ohba", Genre = "Thriller", Volumes = 12, Stock = 18, Rating = 4.9 },
            new Product { Id = "p10", Name = "Fullmetal Alchemist Vol. 1", Price = 559m, Short = "The Elric brothers' alchemical journey", Description = "Edward and Alphonse Elric attempt to use alchemy to bring their mother back to life with devastating consequences.", Image = "products/images/fullmetal-alchemist.jpg", Author = "Hiromu Arakawa", Genre = "Fantasy", Volumes = 1, Stock = 22, Rating = 4.8 },
            new Product { Id = "p11", Name = "Your Name Light Novel", Price = 727m, Short = "The story behind the hit anime film", Description = "The original light novel that inspired the beloved anime film about two teenagers who mysteriously swap bodies.", Image = "products/images/your-name.jpg", Author = "Makoto Shinkai", Genre = "Romance", Volumes = 1, Stock = 35, Rating = 4.6 },
            new Product { Id = "p12", Name = "Spirited Away Picture Book", Price = 1119m, Short = "Studio Ghibli's masterpiece in book form", Description = "A beautifully illustrated picture book adaptation of Hayao Miyazaki's Academy Award-winning film.", Image = "products/images/spirited-away.jpg", Author = "Hayao Miyazaki", Genre = "Fantasy", Volumes = 1, Stock = 5, Rating = 4.7 },
            new Product { Id = "p13", Name = "Wistoria: Wand and Sword Season 1 Blu-ray", Price = 2799m, Short = "Magic academy action anime complete season", Description = "Will Serfort enrolls in a prestigious magic academy despite being unable to use magic, relying solely on his sword skills to prove himself among talented mages.", Image = "products/images/wistoria.jpg", Author = "Fujino Omori & Toshi Aoi", Genre = "Action", Volumes = 1, Stock = 25, Rating = 4.5 },
            new Product { Id = "p14", Name = "Gachiakuta Vol. 1-6 Collection", Price = 3919m, Short = "Post-apocalyptic action manga bundle", Description = "Follow Rudo as he's exiled to the trash-filled abyss and discovers mysterious powers. Complete volumes 1-6 from Kei Urana's acclaimed dark fantasy series.", Image = "products/images/gachiakuta.jpg", Author = "Kei Urana", Genre = "Action", Volumes = 6, Stock = 18, Rating = 4.7 },
            new Product { Id = "p15", Name = "DanDaDan Vol. 1-8 Ultimate Box Set", Price = 5039m, Short = "Aliens, ghosts, and supernatural chaos", Description = "Momo and Okarun's wild adventures battling aliens and spirits. Deluxe box set featuring volumes 1-8 of Yukinobu Tatsu's hit supernatural action-comedy manga.", Image = "products/images/dandandan.jpg", Author = "Yukinobu Tatsu", Genre = "Action", Volumes = 8, Stock = 14, Rating = 4.8 },
            new Product { Id = "p16", Name = "Tokyo Revengers Vol. 1-2", Price = 1119m, Short = "Time-traveling delinquent action", Description = "Takemichi Hanagaki travels back in time to save his ex-girlfriend from a tragic fate. First two volumes of Ken Wakui's thrilling time-leap gang story.", Image = "products/images/tokyorevengers.jpg", Author = "Ken Wakui", Genre = "Action", Volumes = 2, Stock = 32, Rating = 4.6 }
        );
    }
}

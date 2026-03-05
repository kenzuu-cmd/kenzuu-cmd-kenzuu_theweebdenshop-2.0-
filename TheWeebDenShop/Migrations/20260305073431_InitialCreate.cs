using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheWeebDenShop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Short = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Image = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Author = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Genre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Volumes = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "Genre", "Image", "Name", "Price", "Rating", "Short", "Stock", "Volumes" },
                values: new object[,]
                {
                    { "p1", "Koyoharu Gotouge", "Complete 23-volume Demon Slayer manga box set with premium storage case. Follow Tanjiro's entire journey from beginning to end.", "Action", "products/images/demon-slayer.jpg", "Demon Slayer Box Set (Vol.1–23)", 10640m, 4.7999999999999998, "Kimetsu no Yaiba complete manga box set", 12, 23 },
                    { "p10", "Hiromu Arakawa", "Edward and Alphonse Elric attempt to use alchemy to bring their mother back to life with devastating consequences.", "Fantasy", "products/images/fullmetal-alchemist.jpg", "Fullmetal Alchemist Vol. 1", 559m, 4.7999999999999998, "The Elric brothers' alchemical journey", 22, 1 },
                    { "p11", "Makoto Shinkai", "The original light novel that inspired the beloved anime film about two teenagers who mysteriously swap bodies.", "Romance", "products/images/your-name.jpg", "Your Name Light Novel", 727m, 4.5999999999999996, "The story behind the hit anime film", 35, 1 },
                    { "p12", "Hayao Miyazaki", "A beautifully illustrated picture book adaptation of Hayao Miyazaki's Academy Award-winning film.", "Fantasy", "products/images/spirited-away.jpg", "Spirited Away Picture Book", 1119m, 4.7000000000000002, "Studio Ghibli's masterpiece in book form", 5, 1 },
                    { "p13", "Fujino Omori & Toshi Aoi", "Will Serfort enrolls in a prestigious magic academy despite being unable to use magic, relying solely on his sword skills to prove himself among talented mages.", "Action", "products/images/wistoria.jpg", "Wistoria: Wand and Sword Season 1 Blu-ray", 2799m, 4.5, "Magic academy action anime complete season", 25, 1 },
                    { "p14", "Kei Urana", "Follow Rudo as he's exiled to the trash-filled abyss and discovers mysterious powers. Complete volumes 1-6 from Kei Urana's acclaimed dark fantasy series.", "Action", "products/images/gachiakuta.jpg", "Gachiakuta Vol. 1-6 Collection", 3919m, 4.7000000000000002, "Post-apocalyptic action manga bundle", 18, 6 },
                    { "p15", "Yukinobu Tatsu", "Momo and Okarun's wild adventures battling aliens and spirits. Deluxe box set featuring volumes 1-8 of Yukinobu Tatsu's hit supernatural action-comedy manga.", "Action", "products/images/dandandan.jpg", "DanDaDan Vol. 1-8 Ultimate Box Set", 5039m, 4.7999999999999998, "Aliens, ghosts, and supernatural chaos", 14, 8 },
                    { "p16", "Ken Wakui", "Takemichi Hanagaki travels back in time to save his ex-girlfriend from a tragic fate. First two volumes of Ken Wakui's thrilling time-leap gang story.", "Action", "products/images/tokyorevengers.jpg", "Tokyo Revengers Vol. 1-2", 1119m, 4.5999999999999996, "Time-traveling delinquent action", 32, 2 },
                    { "p2", "Gege Akutami", "Yuji Itadori swallows Sukuna's finger and begins his chaotic days at Tokyo Jujutsu High School.", "Action", "products/images/jujutsu-kaisen.jpg", "Jujutsu Kaisen Vol. 1", 503m, 4.5999999999999996, "Yuji Itadori begins his journey", 34, 1 },
                    { "p3", "Tatsuki Fujimoto", "Denji and Pochita's strange partnership. A bloody yet heartbreaking story collected in Part 1 box set.", "Action", "products/images/chainsaw-man.jpg", "Chainsaw Man Box Set (Vol.1–11)", 4479m, 4.7000000000000002, "Chainsaw Man Part 1 box set", 0, 11 },
                    { "p4", "Hajime Isayama", "When titans breach Wall Maria, Eren Yeager witnesses unimaginable horror and vows revenge against the titans.", "Action", "products/images/attack-on-titan.jpg", "Attack on Titan Vol. 1", 559m, 4.9000000000000004, "Humanity's fight for survival begins", 7, 1 },
                    { "p5", "Kohei Horikoshi", "Izuku Midoriya dreams of becoming a hero despite being born without a Quirk in a world where superpowers are common.", "Action", "products/images/my-hero-academia.jpg", "My Hero Academia Vol. 1", 503m, 4.5, "Izuku's journey to become a hero", 30, 1 },
                    { "p6", "Eiichiro Oda", "Monkey D. Luffy sets out to sea to find the legendary treasure One Piece and become the next Pirate King.", "Adventure", "products/images/one-piece.jpg", "One Piece Vol. 1", 559m, 4.7999999999999998, "Luffy begins his pirate adventure", 40, 1 },
                    { "p7", "Masashi Kishimoto", "Follow Naruto Uzumaki from his academy days through the Chunin Exams in this comprehensive box set.", "Action", "products/images/naruto.jpg", "Naruto Box Set 1 (Vol.1–27)", 8959m, 4.7000000000000002, "First Naruto box set collection", 15, 27 },
                    { "p8", "Sui Ishida", "College student Ken Kaneki's life changes forever when he encounters a ghoul and becomes a half-ghoul himself.", "Horror", "products/images/tokyo-ghoul.jpg", "Tokyo Ghoul Vol. 1", 503m, 4.4000000000000004, "Ken Kaneki's transformation begins", 0, 1 },
                    { "p9", "Tsugumi Ohba", "The complete Death Note series featuring Light Yagami's psychological battle with the detective L.", "Thriller", "products/images/death-note.jpg", "Death Note Box Set", 3079m, 4.9000000000000004, "Complete Death Note collection", 18, 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

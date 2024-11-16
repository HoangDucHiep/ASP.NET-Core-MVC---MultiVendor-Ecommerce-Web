using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BannerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Order_ParentOrderId",
                        column: x => x.ParentOrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_Vendors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sale = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasVariant = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    VariantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantsOption",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sale = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantsOption", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_ProductVariantsOption_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VariantOptionID = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_ProductVariantsOption_VariantOptionID",
                        column: x => x.VariantOptionID,
                        principalTable: "ProductVariantsOption",
                        principalColumn: "OptionId");
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VariantOptionID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_ProductVariantsOption_VariantOptionID",
                        column: x => x.VariantOptionID,
                        principalTable: "ProductVariantsOption",
                        principalColumn: "OptionId");
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a", 0, null, "", "ApplicationUser", "truffles@example.com", true, "Truffles Vendor", null, false, null, "TRUFFLES@EXAMPLE.COM", "TRUFFLES_VENDOR", "AQAAAAIAAYagAAAAEJbJpH0pWF3AVscCQ6JfMRp3yIs6VWgdyYEHp0fBTsW1sH1c2QDcvK/c2qnxElN6gw==", null, false, "", false, "truffles_vendor" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "BannerImage", "CreatedAt", "Name", "Slug", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/10/shop-head-bg-2.jpg?fit=1140%2C260&ssl=1", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Home & Garden", "home-garden-1", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electronics", "electronics-2", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/10/shop-head-bg-3.jpg?fit=1140%2C260&ssl=1", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fashion", "fashion-3", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jewelry & Accessories", "jewelry-accessories-4", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports & Entertainment", "sports-entertainment-5", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mother & Kids", "mother-kids-6", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beauty & Health", "beauty-health-7", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toys & Games", "toys-games-8", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automobiles & Motorcycles", "automobiles-motorcycles-9", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Collectibles & Art", "collectibles-art-10", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tools & Home Improvement", "tools-home-improvement-11", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "CreatedAt", "Name", "Status", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Truffles", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "Description", "HasVariant", "Name", "Price", "SKU", "Sale", "Slug", "Status", "Stock", "UpdatedAt", "VendorId" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Batman Death Metal DC Comics Batman Figure", 100m, "SKU-1", 0m, "batman-figure-metal-1", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Minions Toy with Buildable Figures (876 Pieces)", 200m, "SKU-2", 0m, "minions-toy-figures-2", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Masters of the Universe Origins Skeletor Action Figure", 300m, "SKU-3", 26m, "skeletor-action-figure-3", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet-4, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", true, "Apple – iPhone 11 64GB", null, "SKU-4", 0m, "iphone-11-64gb", "active", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageId", "ImageUrl", "IsMain", "ProductId", "VariantOptionID" },
                values: new object[,]
                {
                    { 1, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/4-1.jpg?fit=1400%2C1400&ssl=1", true, 1, null },
                    { 2, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/2-2.jpg?fit=1400%2C1400&ssl=1", false, 1, null },
                    { 3, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/1-2.jpg?fit=1400%2C1400&ssl=1", false, 1, null },
                    { 4, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/3-2.jpg?fit=1400%2C1400&ssl=1", false, 1, null },
                    { 5, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/1-73.jpg?fit=1400%2C1400&ssl=1", true, 2, null },
                    { 6, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/3-54.jpg?fit=1400%2C1400&ssl=1", false, 2, null },
                    { 7, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/4-37.jpg?fit=1400%2C1400&ssl=1", false, 2, null },
                    { 8, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/1-71.jpg?fit=1400%2C1400&ssl=1", true, 3, null },
                    { 9, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/2-61.jpg?fit=1400%2C1400&ssl=1", false, 3, null },
                    { 10, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/3-52.jpg?fit=1400%2C1400&ssl=1", false, 3, null }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "VariantId", "CreatedAt", "Name", "ProductId", "Status", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Color", 4, "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ProductVariantsOption",
                columns: new[] { "OptionId", "CreatedAt", "Price", "SKU", "Sale", "Status", "Stock", "UpdatedAt", "Value", "VariantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 400m, "SKU-4-Black", 20m, "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black", 1 },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 450m, "SKU-4-White", 10m, "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red", 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageId", "ImageUrl", "IsMain", "ProductId", "VariantOptionID" },
                values: new object[,]
                {
                    { 11, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2019/01/1.jpeg?fit=1400%2C1400&ssl=1", true, 4, 1 },
                    { 12, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2019/01/2.jpeg?fit=1400%2C1400&ssl=1", false, 4, 1 },
                    { 13, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2019/01/3.jpeg?fit=1400%2C1400&ssl=1", false, 4, 1 },
                    { 14, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2023/02/1-1.jpg?fit=1400%2C1400&ssl=1", false, 4, 2 },
                    { 15, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2023/02/2-1.jpg?fit=1400%2C1400&ssl=1", false, 4, 2 },
                    { 16, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2023/02/3-1.jpg?fit=1400%2C1400&ssl=1", false, 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ParentOrderId",
                table: "Order",
                column: "ParentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_VariantOptionID",
                table: "ProductImages",
                column: "VariantOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_VendorId",
                table: "Products",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantsOption_VariantId",
                table: "ProductVariantsOption",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_VariantOptionID",
                table: "ShoppingCarts",
                column: "VariantOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors",
                column: "UserId");
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
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProductVariantsOption");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

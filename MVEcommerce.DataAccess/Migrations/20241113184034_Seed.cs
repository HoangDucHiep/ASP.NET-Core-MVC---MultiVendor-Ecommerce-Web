using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Truffles", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "61c08bd7-c3e2-4a64-9054-6b5f9a4fd13c" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "Description", "HasVariant", "Name", "Price", "SKU", "Sale", "Slug", "Status", "Stock", "UpdatedAt", "VendorId" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Batman Death Metal DC Comics Batman Figure", 100m, "SKU-1", 0m, "batman-figure-metal-1", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Minions Toy with Buildable Figures (876 Pieces)", 200m, "SKU-2", 0m, "minions-toy-figures-2", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Masters of the Universe Origins Skeletor Action Figure", 300m, "SKU-3", 26m, "skeletor-action-figure-3", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet-4, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", true, "Apple – iPhone 11 64GB", null, null, null, "iphone-11-64gb", "active", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
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
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 400m, "SKU-4-Black", null, "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black", 1 },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 450m, "SKU-4-White", null, "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red", 1 }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductVariantsOption",
                keyColumn: "OptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVariantsOption",
                keyColumn: "OptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "VariantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: 1);
        }
    }
}

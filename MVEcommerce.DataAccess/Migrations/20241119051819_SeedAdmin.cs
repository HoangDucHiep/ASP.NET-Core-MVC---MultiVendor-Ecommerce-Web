using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a", 0, null, "", "ApplicationUser", "truffles@example.com", true, "Truffles Vendor", null, false, null, "TRUFFLES@EXAMPLE.COM", "TRUFFLES_VENDOR", "AQAAAAIAAYagAAAAEOh0rMePErj0/kpjmnvOSQ7OMLAjDDIU0WNukR3QB93OW/n82wVEJSncPFTTCZAD7A==", null, false, "", false, "truffles_vendor" });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "Avartar", "Banner", "CreatedAt", "Name", "Status", "UpdatedAt", "UserId" },
                values: new object[] { 1, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Truffles", "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "Description", "HasVariant", "Name", "Price", "Sale", "Slug", "Status", "Stock", "UpdatedAt", "VendorId" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Batman Death Metal DC Comics Batman Figure", 100m, 0m, "batman-figure-metal-1", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Minions Toy with Buildable Figures (876 Pieces)", 200m, 0m, "minions-toy-figures-2", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Masters of the Universe Origins Skeletor Action Figure", 300m, 26m, "skeletor-action-figure-3", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet-4, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", true, "Apple – iPhone 11 64GB", null, 0m, "iphone-11-64gb", "active", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
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
                columns: new[] { "OptionId", "CreatedAt", "Price", "Sale", "Status", "Stock", "UpdatedAt", "Value", "VariantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 400m, 20m, "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black", 1 },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 450m, 10m, "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red", 1 }
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
    }
}

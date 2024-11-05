using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CategoryId", "Description", "Name", "Slug" },
                values: new object[] { 8, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", "Batman Death Metal DC Comics Batman Figure", "batman-figure-metal" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "Description", "HasVariant", "Name", "Price", "SKU", "Slug", "Status", "Stock", "UpdatedAt", "VendorId" },
                values: new object[,]
                {
                    { 2, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Minions Toy with Buildable Figures (876 Pieces)", 200m, "SKU-2", "minions-toy-figures", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Masters of the Universe Origins Skeletor Action Figure", 300m, "SKU-3", "skeletor-action-figure", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.UpdateData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: 1,
                columns: new[] { "Address", "Name" },
                values: new object[] { "1 New Buildings, Dunwear Bridgwater, United Kingdom (UK)", "Truffles" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "CategoryId", "Description", "Name", "Slug" },
                values: new object[] { 1, "Description of Product 1", "Product 1", "product-1" });

            migrationBuilder.UpdateData(
                table: "Vendors",
                keyColumn: "VendorId",
                keyValue: 1,
                columns: new[] { "Address", "Name" },
                values: new object[] { "123 Street", "Vendor 1" });
        }
    }
}

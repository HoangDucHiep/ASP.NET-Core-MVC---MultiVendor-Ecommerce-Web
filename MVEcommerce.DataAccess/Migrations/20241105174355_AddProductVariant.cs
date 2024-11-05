using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariant_VariantId",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "ProductImages",
                newName: "VariantOptionID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_VariantId",
                table: "ProductImages",
                newName: "IX_ProductImages_VariantOptionID");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "Description", "HasVariant", "Name", "Price", "SKU", "Slug", "Status", "Stock", "UpdatedAt", "VendorId" },
                values: new object[] { 4, 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", false, "Apple – iPhone 11 64GB", 400m, "SKU-4", "iphone-11-64gb", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "ProductVariant",
                columns: new[] { "VariantId", "CreatedAt", "Name", "ProductId", "Status", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Color", 4, "active", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ProductVariantOption",
                columns: new[] { "OptionId", "CreatedAt", "Price", "SKU", "Status", "Stock", "UpdatedAt", "Value", "VariantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 400m, "SKU-4-Black", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black", 1 },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 450m, "SKU-4-White", "active", 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red", 1 }
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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductVariantOption_VariantOptionID",
                table: "ProductImages",
                column: "VariantOptionID",
                principalTable: "ProductVariantOption",
                principalColumn: "OptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariantOption_VariantOptionID",
                table: "ProductImages");

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
                table: "ProductVariantOption",
                keyColumn: "OptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVariantOption",
                keyColumn: "OptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVariant",
                keyColumn: "VariantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "VariantOptionID",
                table: "ProductImages",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_VariantOptionID",
                table: "ProductImages",
                newName: "IX_ProductImages_VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductVariant_VariantId",
                table: "ProductImages",
                column: "VariantId",
                principalTable: "ProductVariant",
                principalColumn: "VariantId");
        }
    }
}

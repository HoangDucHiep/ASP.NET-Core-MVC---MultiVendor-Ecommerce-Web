using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSKU : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SKU",
                table: "ProductVariantsOption");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEC7miI0T7WwRFa940aJ4WcEN4xwoFW8kLN7icVWQgBkhBfchbAp9NMdnTEh37b7Rog==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "ProductVariantsOption",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDTouJmAEsj+oXagEMvmUza6qb84qOMuuxIsBZsW1jW+dKq5MsuNLec+pDKVCi8dlQ==");

            migrationBuilder.UpdateData(
                table: "ProductVariantsOption",
                keyColumn: "OptionId",
                keyValue: 1,
                column: "SKU",
                value: "SKU-4-Black");

            migrationBuilder.UpdateData(
                table: "ProductVariantsOption",
                keyColumn: "OptionId",
                keyValue: 2,
                column: "SKU",
                value: "SKU-4-White");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "SKU",
                value: "SKU-1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "SKU",
                value: "SKU-2");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "SKU",
                value: "SKU-3");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "SKU",
                value: "SKU-4");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Seed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductVariantsOption",
                keyColumn: "OptionId",
                keyValue: 1,
                column: "Sale",
                value: 20m);

            migrationBuilder.UpdateData(
                table: "ProductVariantsOption",
                keyColumn: "OptionId",
                keyValue: 2,
                column: "Sale",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "SKU",
                value: "SKU-4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductVariantsOption",
                keyColumn: "OptionId",
                keyValue: 1,
                column: "Sale",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductVariantsOption",
                keyColumn: "OptionId",
                keyValue: 2,
                column: "Sale",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "SKU",
                value: null);
        }
    }
}

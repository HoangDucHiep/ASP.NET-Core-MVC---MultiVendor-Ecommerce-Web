using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderDetail2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VariantOptionId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "OptionName",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENUeRqNTryZ9rHel9wPTPsUmPa9aLEvf+aKjotoTiYQ3M7eWmxZSsS5+e8sZydXC2w==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionName",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "VariantOptionId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPhBdxafkl64cNWcxEKzYAmJOmZLrBmtvIrgISN7j5PSSi/XfE2vptYouu3n3mQ0FA==");
        }
    }
}

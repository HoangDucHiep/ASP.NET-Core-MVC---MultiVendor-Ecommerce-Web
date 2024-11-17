using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderDetai3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMQ/RGhpjMIaNSJwYtvrGI9t+bYnY4u/O0eZDYzoTD5TOGc5/JBCB4rvzZdY8jFMWQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Order");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENUeRqNTryZ9rHel9wPTPsUmPa9aLEvf+aKjotoTiYQ3M7eWmxZSsS5+e8sZydXC2w==");
        }
    }
}

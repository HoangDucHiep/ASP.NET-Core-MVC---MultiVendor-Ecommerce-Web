using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDTouJmAEsj+oXagEMvmUza6qb84qOMuuxIsBZsW1jW+dKq5MsuNLec+pDKVCi8dlQ==");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_VendorId",
                table: "Addresses",
                column: "VendorId",
                unique: true,
                filter: "[VendorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Vendors_VendorId",
                table: "Addresses",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Vendors_VendorId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_VendorId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMQ/RGhpjMIaNSJwYtvrGI9t+bYnY4u/O0eZDYzoTD5TOGc5/JBCB4rvzZdY8jFMWQ==");
        }
    }
}

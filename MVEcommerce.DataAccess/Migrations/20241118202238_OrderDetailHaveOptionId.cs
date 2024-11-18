using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OrderDetailHaveOptionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OptionId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOh0rMePErj0/kpjmnvOSQ7OMLAjDDIU0WNukR3QB93OW/n82wVEJSncPFTTCZAD7A==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEC7miI0T7WwRFa940aJ4WcEN4xwoFW8kLN7icVWQgBkhBfchbAp9NMdnTEh37b7Rog==");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOQhUIrZCRU/YnLAGZExxKuXnPsMhxI1OZdX5dxyiZB27LNGxI9nc08qvANJAwfx2Q==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJbJpH0pWF3AVscCQ6JfMRp3yIs6VWgdyYEHp0fBTsW1sH1c2QDcvK/c2qnxElN6gw==");
        }
    }
}

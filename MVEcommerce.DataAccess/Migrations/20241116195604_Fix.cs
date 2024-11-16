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
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Addresses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEINYc3yR5RfhvGI1z1jE5aIMhw+l7L72WAQ8z+RbVkcm4cVPzGOqhkfO9RVD9/SzDA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc3ed980-19bd-4ba1-96f2-b72fed4ec54a",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPgpbj0PRk1WMZGraMQ104UYOkn2n2MUBB5FQA1i8Ah1CxWjD0cmwKDZ1ak2YwR88A==");
        }
    }
}

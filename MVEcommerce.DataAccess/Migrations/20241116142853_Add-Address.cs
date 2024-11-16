using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    public partial class AddAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Vendors",
                type: "int",
                nullable: true); // Allow null initially to avoid conflicts

            migrationBuilder.AddColumn<string>(
                name: "Avartar",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Banner",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            // Update existing data to avoid foreign key conflicts
            migrationBuilder.Sql("UPDATE Vendors SET AddressId = NULL WHERE AddressId NOT IN (SELECT AddressId FROM Addresses)");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_AddressId",
                table: "Vendors",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Addresses_AddressId",
                table: "Vendors",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.SetNull); // Set to null on delete to avoid conflicts
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Addresses_AddressId",
                table: "Vendors");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_AddressId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Avartar",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Banner",
                table: "Vendors");
        }
    }
}
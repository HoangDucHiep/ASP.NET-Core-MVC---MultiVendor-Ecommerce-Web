using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreProductImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_ProductVariant_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "VariantId");
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageId", "ImageUrl", "IsMain", "ProductId", "VariantId" },
                values: new object[,]
                {
                    { 1, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/4-1.jpg?fit=1400%2C1400&ssl=1", true, 1, null },
                    { 2, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/2-2.jpg?fit=1400%2C1400&ssl=1", false, 1, null },
                    { 3, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/1-2.jpg?fit=1400%2C1400&ssl=1", false, 1, null },
                    { 4, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/3-2.jpg?fit=1400%2C1400&ssl=1", false, 1, null },
                    { 5, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/1-73.jpg?fit=1400%2C1400&ssl=1", true, 2, null },
                    { 6, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/3-54.jpg?fit=1400%2C1400&ssl=1", false, 2, null },
                    { 7, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/4-37.jpg?fit=1400%2C1400&ssl=1", false, 2, null },
                    { 8, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/1-71.jpg?fit=1400%2C1400&ssl=1", true, 3, null },
                    { 9, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/2-61.jpg?fit=1400%2C1400&ssl=1", false, 3, null },
                    { 10, "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/3-52.jpg?fit=1400%2C1400&ssl=1", false, 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_VariantId",
                table: "ProductImages",
                column: "VariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");
        }
    }
}

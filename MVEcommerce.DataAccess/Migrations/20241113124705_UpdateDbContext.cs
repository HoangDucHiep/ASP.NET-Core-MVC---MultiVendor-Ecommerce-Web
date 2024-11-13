using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariantOption_VariantOptionID",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Products_ProductId",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantOption_ProductVariant_VariantId",
                table: "ProductVariantOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantOption",
                table: "ProductVariantOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant");

            migrationBuilder.RenameTable(
                name: "ProductVariantOption",
                newName: "ProductVariantsOption");

            migrationBuilder.RenameTable(
                name: "ProductVariant",
                newName: "ProductVariants");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantOption_VariantId",
                table: "ProductVariantsOption",
                newName: "IX_ProductVariantsOption_VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantsOption",
                table: "ProductVariantsOption",
                column: "OptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductVariantsOption_VariantOptionID",
                table: "ProductImages",
                column: "VariantOptionID",
                principalTable: "ProductVariantsOption",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantsOption_ProductVariants_VariantId",
                table: "ProductVariantsOption",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "VariantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariantsOption_VariantOptionID",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantsOption_ProductVariants_VariantId",
                table: "ProductVariantsOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantsOption",
                table: "ProductVariantsOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants");

            migrationBuilder.RenameTable(
                name: "ProductVariantsOption",
                newName: "ProductVariantOption");

            migrationBuilder.RenameTable(
                name: "ProductVariants",
                newName: "ProductVariant");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantsOption_VariantId",
                table: "ProductVariantOption",
                newName: "IX_ProductVariantOption_VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariant",
                newName: "IX_ProductVariant_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantOption",
                table: "ProductVariantOption",
                column: "OptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductVariantOption_VariantOptionID",
                table: "ProductImages",
                column: "VariantOptionID",
                principalTable: "ProductVariantOption",
                principalColumn: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Products_ProductId",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantOption_ProductVariant_VariantId",
                table: "ProductVariantOption",
                column: "VariantId",
                principalTable: "ProductVariant",
                principalColumn: "VariantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

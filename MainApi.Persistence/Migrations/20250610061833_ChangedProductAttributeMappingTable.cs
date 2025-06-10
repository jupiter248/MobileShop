using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProductAttributeMappingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributeMappings_Product_ProductAttribute_MappingId",
                table: "ProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValues_Product_ProductAttribute_MappingId",
                table: "ProductAttributeValues");

            migrationBuilder.DropColumn(
                name: "Product_ProductAttribute_MappingId",
                table: "ProductAttributeValues");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductAttributeMappingId",
                table: "ProductAttributeValues",
                column: "ProductAttributeMappingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributeMappings_ProductAttributeMappingId",
                table: "ProductAttributeValues",
                column: "ProductAttributeMappingId",
                principalTable: "ProductAttributeMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributeMappings_ProductAttributeMappingId",
                table: "ProductAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributeValues_ProductAttributeMappingId",
                table: "ProductAttributeValues");

            migrationBuilder.AddColumn<int>(
                name: "Product_ProductAttribute_MappingId",
                table: "ProductAttributeValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_Product_ProductAttribute_MappingId",
                table: "ProductAttributeValues",
                column: "Product_ProductAttribute_MappingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValues_ProductAttributeMappings_Product_ProductAttribute_MappingId",
                table: "ProductAttributeValues",
                column: "Product_ProductAttribute_MappingId",
                principalTable: "ProductAttributeMappings",
                principalColumn: "Id");
        }
    }
}

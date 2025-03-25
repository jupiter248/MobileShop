using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainApi.Migrations
{
    /// <inheritdoc />
    public partial class FixedProductCombinationBugs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttributeCombinations");

            migrationBuilder.CreateTable(
                name: "ProductCombinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCombinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCombinations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCombinationsAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCombinationId = table.Column<int>(type: "int", nullable: false),
                    AttributeValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCombinationsAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCombinationsAttribute_PredefinedProductAttributeValues_AttributeValueId",
                        column: x => x.AttributeValueId,
                        principalTable: "PredefinedProductAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCombinationsAttribute_ProductCombinations_ProductCombinationId",
                        column: x => x.ProductCombinationId,
                        principalTable: "ProductCombinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCombinations_ProductId",
                table: "ProductCombinations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCombinationsAttribute_AttributeValueId",
                table: "ProductCombinationsAttribute",
                column: "AttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCombinationsAttribute_ProductCombinationId",
                table: "ProductCombinationsAttribute",
                column: "ProductCombinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCombinationsAttribute");

            migrationBuilder.DropTable(
                name: "ProductCombinations");

            migrationBuilder.CreateTable(
                name: "ProductAttributeCombinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AttributeCombination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeCombinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeCombinations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeCombinations_ProductId",
                table: "ProductAttributeCombinations",
                column: "ProductId");
        }
    }
}

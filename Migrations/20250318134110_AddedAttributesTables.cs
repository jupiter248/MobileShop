using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MainApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttributesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "696d1e5a-d242-42f8-8eac-072732b1426c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6acdabba-b189-4a78-b22a-5b8c1c7ff4eb");

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PredefinedProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredefinedProductAttributeValues_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeMappings_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributeMappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationAttributeOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificationAttributeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationAttributeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecificationAttributeOptions_SpecificationAttributes_SpecificationAttributeId",
                        column: x => x.SpecificationAttributeId,
                        principalTable: "SpecificationAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeMappingId = table.Column<int>(type: "int", nullable: false),
                    Product_ProductAttribute_MappingId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_ProductAttributeMappings_Product_ProductAttribute_MappingId",
                        column: x => x.Product_ProductAttribute_MappingId,
                        principalTable: "ProductAttributeMappings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecificationAttributeMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SpecificationAttributeOptionId = table.Column<int>(type: "int", nullable: false),
                    AllowFiltering = table.Column<bool>(type: "bit", nullable: false),
                    ShowOnProductPage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationAttributeMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecificationAttributeMappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificationAttributeMappings_SpecificationAttributeOptions_SpecificationAttributeOptionId",
                        column: x => x.SpecificationAttributeOptionId,
                        principalTable: "SpecificationAttributeOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d1685655-7fdc-4bea-ae4c-63a76d565e36", null, "Admin", "ADMIN" },
                    { "d8a73639-e898-4ee0-b96c-2e0706ff12e7", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PredefinedProductAttributeValues_ProductAttributeId",
                table: "PredefinedProductAttributeValues",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeMappings_ProductAttributeId",
                table: "ProductAttributeMappings",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeMappings_ProductId",
                table: "ProductAttributeMappings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_Product_ProductAttribute_MappingId",
                table: "ProductAttributeValues",
                column: "Product_ProductAttribute_MappingId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationAttributeMappings_ProductId",
                table: "SpecificationAttributeMappings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationAttributeMappings_SpecificationAttributeOptionId",
                table: "SpecificationAttributeMappings",
                column: "SpecificationAttributeOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationAttributeOptions_SpecificationAttributeId",
                table: "SpecificationAttributeOptions",
                column: "SpecificationAttributeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredefinedProductAttributeValues");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "SpecificationAttributeMappings");

            migrationBuilder.DropTable(
                name: "ProductAttributeMappings");

            migrationBuilder.DropTable(
                name: "SpecificationAttributeOptions");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "SpecificationAttributes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1685655-7fdc-4bea-ae4c-63a76d565e36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8a73639-e898-4ee0-b96c-2e0706ff12e7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "696d1e5a-d242-42f8-8eac-072732b1426c", null, "Admin", "ADMIN" },
                    { "6acdabba-b189-4a78-b22a-5b8c1c7ff4eb", null, "User", "USER" }
                });
        }
    }
}

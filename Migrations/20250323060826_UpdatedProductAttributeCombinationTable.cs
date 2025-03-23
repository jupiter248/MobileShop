using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MainApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProductAttributeCombinationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08c30b9b-a8a2-47f0-a4b9-cc220f9235ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "355f53d7-51fb-4bed-85fb-6d7013579195");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08c30b9b-a8a2-47f0-a4b9-cc220f9235ea", null, "User", "USER" },
                    { "355f53d7-51fb-4bed-85fb-6d7013579195", null, "Admin", "ADMIN" }
                });
        }
    }
}

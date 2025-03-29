using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedShippingStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentItems_OrderShipments_OrderShipmentId",
                table: "ShipmentItems");

            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "ShippingStatuses",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "OrderShipmentId",
                table: "ShipmentItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "OrderShipments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentItems_OrderShipments_OrderShipmentId",
                table: "ShipmentItems",
                column: "OrderShipmentId",
                principalTable: "OrderShipments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentItems_OrderShipments_OrderShipmentId",
                table: "ShipmentItems");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ShippingStatuses",
                newName: "StatusName");

            migrationBuilder.AlterColumn<int>(
                name: "OrderShipmentId",
                table: "ShipmentItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "OrderShipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentItems_OrderShipments_OrderShipmentId",
                table: "ShipmentItems",
                column: "OrderShipmentId",
                principalTable: "OrderShipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

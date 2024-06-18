using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class CartIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_DeliveryDatas_DeliveryId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "Customers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_DeliveryDatas_DeliveryId",
                table: "Bills",
                column: "DeliveryId",
                principalTable: "DeliveryDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_DeliveryDatas_DeliveryId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills");

            migrationBuilder.AlterColumn<Guid>(
                name: "CartId",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_DeliveryDatas_DeliveryId",
                table: "Bills",
                column: "DeliveryId",
                principalTable: "DeliveryDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

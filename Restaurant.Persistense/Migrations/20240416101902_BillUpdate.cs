using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class BillUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipsPercents",
                table: "Bills");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Bills",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Tips",
                table: "Bills",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Tips",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "TipsPercents",
                table: "Bills",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

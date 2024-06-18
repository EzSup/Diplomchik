using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class tableNumAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Num",
                table: "Tables",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Num",
                table: "Tables");
        }
    }
}

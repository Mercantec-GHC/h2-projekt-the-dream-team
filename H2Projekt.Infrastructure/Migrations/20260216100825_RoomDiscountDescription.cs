using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2Projekt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoomDiscountDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "RoomDiscounts",
                newName: "Percentage");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RoomDiscounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RoomDiscounts");

            migrationBuilder.RenameColumn(
                name: "Percentage",
                table: "RoomDiscounts",
                newName: "Discount");
        }
    }
}

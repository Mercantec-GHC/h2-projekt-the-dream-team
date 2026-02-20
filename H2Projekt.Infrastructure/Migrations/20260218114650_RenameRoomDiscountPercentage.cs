using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2Projekt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameRoomDiscountPercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "RoomDiscounts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RoomDiscounts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "PricePerNight",
                table: "RoomDiscounts",
                type: "integer",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "RoomDiscounts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RoomDiscounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "RoomDiscounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2Projekt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiscountPricePerNightDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerNight",
                table: "RoomDiscounts",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldPrecision: 10,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PricePerNight",
                table: "RoomDiscounts",
                type: "integer",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2);
        }
    }
}

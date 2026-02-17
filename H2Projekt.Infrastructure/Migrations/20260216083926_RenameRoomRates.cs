using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2Projekt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameRoomRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomRates_RoomTypes_RoomTypeId",
                table: "RoomRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomRates",
                table: "RoomRates");

            migrationBuilder.RenameTable(
                name: "RoomRates",
                newName: "RoomDiscounts");

            migrationBuilder.RenameIndex(
                name: "IX_RoomRates_RoomTypeId",
                table: "RoomDiscounts",
                newName: "IX_RoomDiscounts_RoomTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomDiscounts",
                table: "RoomDiscounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomDiscounts_RoomTypes_RoomTypeId",
                table: "RoomDiscounts",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomDiscounts_RoomTypes_RoomTypeId",
                table: "RoomDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomDiscounts",
                table: "RoomDiscounts");

            migrationBuilder.RenameTable(
                name: "RoomDiscounts",
                newName: "RoomRates");

            migrationBuilder.RenameIndex(
                name: "IX_RoomDiscounts_RoomTypeId",
                table: "RoomRates",
                newName: "IX_RoomRates_RoomTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomRates",
                table: "RoomRates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRates_RoomTypes_RoomTypeId",
                table: "RoomRates",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

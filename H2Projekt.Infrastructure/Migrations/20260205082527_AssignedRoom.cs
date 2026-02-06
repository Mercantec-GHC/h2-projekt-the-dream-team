using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2Projekt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AssignedRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Bookings",
                newName: "RoomType");

            migrationBuilder.RenameColumn(
                name: "CheckOut",
                table: "Bookings",
                newName: "ToDate");

            migrationBuilder.RenameColumn(
                name: "CheckIn",
                table: "Bookings",
                newName: "FromDate");

            migrationBuilder.AddColumn<int>(
                name: "AssignedRoomId",
                table: "Bookings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Number",
                table: "Rooms",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AssignedRoomId",
                table: "Bookings",
                column: "AssignedRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_AssignedRoomId",
                table: "Bookings",
                column: "AssignedRoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_AssignedRoomId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_Number",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AssignedRoomId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AssignedRoomId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "Bookings",
                newName: "CheckOut");

            migrationBuilder.RenameColumn(
                name: "RoomType",
                table: "Bookings",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "FromDate",
                table: "Bookings",
                newName: "CheckIn");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Bookings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2Projekt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameAssignedRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_AssignedRoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "AssignedRoomId",
                table: "Bookings",
                newName: "RoomId1");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_AssignedRoomId",
                table: "Bookings",
                newName: "IX_Bookings_RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId1",
                table: "Bookings",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId1",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "RoomId1",
                table: "Bookings",
                newName: "AssignedRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_RoomId1",
                table: "Bookings",
                newName: "IX_Bookings_AssignedRoomId");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_AssignedRoomId",
                table: "Bookings",
                column: "AssignedRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}

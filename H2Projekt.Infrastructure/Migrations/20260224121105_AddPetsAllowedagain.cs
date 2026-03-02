using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H2Projekt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPetsAllowedagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PetsAllowed",
                table: "RoomTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetsAllowed",
                table: "RoomTypes");
        }
    }
}

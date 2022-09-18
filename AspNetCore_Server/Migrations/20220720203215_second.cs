using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCore_Server.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phonenumber",
                table: "SuperUsers",
                newName: "Lastname");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "SuperUsers",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "SuperUsers",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "SuperUsers",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "SuperUsers");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "SuperUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "SuperUsers");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "SuperUsers",
                newName: "Phonenumber");
        }
    }
}

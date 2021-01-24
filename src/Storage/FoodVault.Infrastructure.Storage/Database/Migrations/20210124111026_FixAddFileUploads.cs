using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodVault.Infrastructure.Storage.Database.Migrations
{
    public partial class FixAddFileUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "FileUploads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "FileUploads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelativeFileLocation",
                table: "FileUploads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "FileUploads",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "FileUploads");
            
            migrationBuilder.DropColumn(
                name: "RelativeFileLocation",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "FileUploads");
        }
    }
}

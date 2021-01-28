using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodVault.Infrastructure.Storage.Database.Migrations
{
    public partial class StorageSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FoodStorages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FoodStorages");
        }
    }
}

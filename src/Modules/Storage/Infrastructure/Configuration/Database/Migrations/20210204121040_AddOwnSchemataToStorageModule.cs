using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Database.Migrations
{
    public partial class AddOwnSchemataToStorageModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "storage");

            migrationBuilder.RenameTable(
                name: "StoredProducts",
                newName: "StoredProducts",
                newSchema: "storage");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "storage");

            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                newName: "OutboxMessages",
                newSchema: "storage");

            migrationBuilder.RenameTable(
                name: "InternalCommands",
                newName: "InternalCommands",
                newSchema: "storage");

            migrationBuilder.RenameTable(
                name: "FoodStorages",
                newName: "FoodStorages",
                newSchema: "storage");

            migrationBuilder.RenameTable(
                name: "FileUploads",
                newName: "FileUploads",
                newSchema: "storage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "StoredProducts",
                schema: "storage",
                newName: "StoredProducts");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "storage",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "OutboxMessages",
                schema: "storage",
                newName: "OutboxMessages");

            migrationBuilder.RenameTable(
                name: "InternalCommands",
                schema: "storage",
                newName: "InternalCommands");

            migrationBuilder.RenameTable(
                name: "FoodStorages",
                schema: "storage",
                newName: "FoodStorages");

            migrationBuilder.RenameTable(
                name: "FileUploads",
                schema: "storage",
                newName: "FileUploads");
        }
    }
}

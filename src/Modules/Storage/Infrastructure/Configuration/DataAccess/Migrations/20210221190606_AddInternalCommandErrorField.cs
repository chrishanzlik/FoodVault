using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.DataAccess.Migrations
{
    public partial class AddInternalCommandErrorField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                schema: "storage",
                table: "InternalCommands",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                schema: "storage",
                table: "InternalCommands");
        }
    }
}

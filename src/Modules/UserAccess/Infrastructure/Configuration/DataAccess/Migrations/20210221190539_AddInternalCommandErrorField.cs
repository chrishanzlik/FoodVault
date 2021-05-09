using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodVault.Modules.UserAccess.Infrastructure.Configuration.DataAccess.Migrations
{
    public partial class AddInternalCommandErrorField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                schema: "users",
                table: "InternalCommands",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                schema: "users",
                table: "InternalCommands");
        }
    }
}

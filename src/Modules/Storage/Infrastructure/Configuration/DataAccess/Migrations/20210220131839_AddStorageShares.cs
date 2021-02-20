using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.DataAccess.Migrations
{
    public partial class AddStorageShares : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorageShares",
                schema: "storage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FoodStorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanWrite = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageShares_FoodStorages_FoodStorageId",
                        column: x => x.FoodStorageId,
                        principalSchema: "storage",
                        principalTable: "FoodStorages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorageShares_FoodStorageId",
                schema: "storage",
                table: "StorageShares",
                column: "FoodStorageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageShares",
                schema: "storage");
        }
    }
}

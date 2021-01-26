using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodVault.Infrastructure.Storage.Database.Migrations
{
    public partial class AddOwnIdToStoredProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StoredProducts",
                table: "StoredProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "StoredProducts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "StoredProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoredProducts",
                table: "StoredProducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StoredProducts_FoodStorageId",
                table: "StoredProducts",
                column: "FoodStorageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StoredProducts",
                table: "StoredProducts");

            migrationBuilder.DropIndex(
                name: "IX_StoredProducts_FoodStorageId",
                table: "StoredProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StoredProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "StoredProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoredProducts",
                table: "StoredProducts",
                columns: new[] { "FoodStorageId", "ProductId" });
        }
    }
}

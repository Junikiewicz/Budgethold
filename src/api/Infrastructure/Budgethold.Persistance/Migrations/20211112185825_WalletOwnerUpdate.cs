using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgethold.Persistance.Migrations
{
    public partial class WalletOwnerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwningUserId",
                table: "Wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_OwningUserId",
                table: "Wallets",
                column: "OwningUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_AspNetUsers_OwningUserId",
                table: "Wallets",
                column: "OwningUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_AspNetUsers_OwningUserId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_OwningUserId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "OwningUserId",
                table: "Wallets");
        }
    }
}

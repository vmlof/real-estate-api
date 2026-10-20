using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homely.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TokenHashMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_refresh_tokens_Token",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "refresh_tokens");

            migrationBuilder.AddColumn<string>(
                name: "TokenHash",
                table: "refresh_tokens",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_TokenHash",
                table: "refresh_tokens",
                column: "TokenHash",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_users_UserId",
                table: "refresh_tokens",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_users_UserId",
                table: "refresh_tokens");

            migrationBuilder.DropIndex(
                name: "IX_refresh_tokens_TokenHash",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "TokenHash",
                table: "refresh_tokens");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "refresh_tokens",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_Token",
                table: "refresh_tokens",
                column: "Token",
                unique: true);
        }
    }
}

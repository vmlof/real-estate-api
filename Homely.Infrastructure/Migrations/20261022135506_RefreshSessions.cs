using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homely.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "refresh_tokens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "refresh_sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refresh_sessions_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Existing refresh tokens each become an independent session.
            migrationBuilder.Sql("""
                INSERT INTO refresh_sessions ("Id", "UserId", "IsRevoked")
                SELECT "Id", "UserId", "IsRevoked" FROM refresh_tokens;

                UPDATE refresh_tokens SET "SessionId" = "Id";
                """);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_SessionId",
                table: "refresh_tokens",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_sessions_UserId",
                table: "refresh_sessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_refresh_sessions_SessionId",
                table: "refresh_tokens",
                column: "SessionId",
                principalTable: "refresh_sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_refresh_sessions_SessionId",
                table: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "refresh_sessions");

            migrationBuilder.DropIndex(
                name: "IX_refresh_tokens_SessionId",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "refresh_tokens");
        }
    }
}

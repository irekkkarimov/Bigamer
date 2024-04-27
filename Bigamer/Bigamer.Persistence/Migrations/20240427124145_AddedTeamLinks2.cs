using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigamer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeamLinks2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamLink_Teams_TeamId",
                table: "TeamLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamLink",
                table: "TeamLink");

            migrationBuilder.RenameTable(
                name: "TeamLink",
                newName: "TeamLinks");

            migrationBuilder.RenameIndex(
                name: "IX_TeamLink_TeamId",
                table: "TeamLinks",
                newName: "IX_TeamLinks_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamLinks",
                table: "TeamLinks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamLinks_Teams_TeamId",
                table: "TeamLinks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamLinks_Teams_TeamId",
                table: "TeamLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamLinks",
                table: "TeamLinks");

            migrationBuilder.RenameTable(
                name: "TeamLinks",
                newName: "TeamLink");

            migrationBuilder.RenameIndex(
                name: "IX_TeamLinks_TeamId",
                table: "TeamLink",
                newName: "IX_TeamLink_TeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamLink",
                table: "TeamLink",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamLink_Teams_TeamId",
                table: "TeamLink",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

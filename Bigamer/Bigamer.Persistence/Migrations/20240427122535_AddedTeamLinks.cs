using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigamer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeamLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "TeamInfos",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeamLink",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<int>(type: "integer", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamLink_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamLink_TeamId",
                table: "TeamLink",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamLink");

            migrationBuilder.DropColumn(
                name: "About",
                table: "TeamInfos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigamer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedMaxTeamCountForGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxTeamCount",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxTeamCount",
                table: "Games");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigamer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlteredSubscriberEnt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationCode",
                table: "Subscribers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSubscribeAttempt",
                table: "Subscribers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscribedAt",
                table: "Subscribers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationCode",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "LastSubscribeAttempt",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "SubscribedAt",
                table: "Subscribers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSettingsProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Settings",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Settings",
                table: "Users");
        }
    }
}

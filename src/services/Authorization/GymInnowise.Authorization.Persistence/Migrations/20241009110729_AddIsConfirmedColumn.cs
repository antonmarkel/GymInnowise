using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymInnowise.Authorization.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsConfirmedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                table: "Accounts");
        }
    }
}

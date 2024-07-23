using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymInnowise.Authorization.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UnnecessaryPropsRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f5784ffe-d280-4347-bf08-61bc8306f785"));

            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "RevokedDate",
                table: "RefreshTokens");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Role" },
                values: new object[] { new Guid("0ed47ccb-8436-4c3c-97c0-aea89186d4ba"), "Client" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0ed47ccb-8436-4c3c-97c0-aea89186d4ba"));

            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "RefreshTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedDate",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Role" },
                values: new object[] { new Guid("f5784ffe-d280-4347-bf08-61bc8306f785"), "Client" });
        }
    }
}

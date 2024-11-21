using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymInnowise.Authorization.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationRoleStringConvConfigure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0ed47ccb-8436-4c3c-97c0-aea89186d4ba"));

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "Role");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Role" },
                values: new object[] { new Guid("22afe0e7-e197-4c3a-a0d4-cb3fda5f8373"), "Client" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22afe0e7-e197-4c3a-a0d4-cb3fda5f8373"));

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { new Guid("0ed47ccb-8436-4c3c-97c0-aea89186d4ba"), "Client" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymInnowise.Authorization.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22afe0e7-e197-4c3a-a0d4-cb3fda5f8373"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Role" },
                values: new object[] { new Guid("5fca7991-1bcd-4ae0-8739-58b297ae51a7"), "Client" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5fca7991-1bcd-4ae0-8739-58b297ae51a7"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Role" },
                values: new object[] { new Guid("22afe0e7-e197-4c3a-a0d4-cb3fda5f8373"), "Client" });
        }
    }
}

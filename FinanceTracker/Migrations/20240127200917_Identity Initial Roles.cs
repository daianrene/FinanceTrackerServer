using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceTracker.Migrations
{
    /// <inheritdoc />
    public partial class IdentityInitialRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01e2689e-b762-4e02-8241-aab046dbe817", null, "User", "USER" },
                    { "9b9457aa-3a99-4537-bdcb-89bb5c8b9761", null, "Admin", "AMDIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01e2689e-b762-4e02-8241-aab046dbe817");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b9457aa-3a99-4537-bdcb-89bb5c8b9761");
        }
    }
}

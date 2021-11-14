using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class RenamedUserneedcategorytousercategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56e4fd39-8115-478c-803d-f69476a0ce3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e465283-0778-454d-a65a-aeba4de6457f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce6b4d07-b0e7-4a51-aca5-59469213eb21");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "61ed755b-e70e-4061-9285-73271cfa28b1", "28576f75-f79c-4200-a1f5-989b723481ef", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "675f0f72-05b7-4972-96fe-47a8a324658a", "e1d30d2c-39e6-4683-bd4c-6e9aa96a94b7", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6882e6d-60f4-495a-8c39-d663d94287cc", "a69117ab-08f6-4c7c-b54b-7b0465485ddf", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61ed755b-e70e-4061-9285-73271cfa28b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "675f0f72-05b7-4972-96fe-47a8a324658a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6882e6d-60f4-495a-8c39-d663d94287cc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "56e4fd39-8115-478c-803d-f69476a0ce3c", "cbfb0132-e6f5-4ffd-83d4-c25458fe6710", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e465283-0778-454d-a65a-aeba4de6457f", "7b812d10-3fb0-4de2-84e8-4b97cc8a9ec6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce6b4d07-b0e7-4a51-aca5-59469213eb21", "3b558fdb-1ee8-40a4-b468-307c4eec7fcf", "User", "USER" });
        }
    }
}

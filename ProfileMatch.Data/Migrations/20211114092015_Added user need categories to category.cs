using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Addeduserneedcategoriestocategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b3c5113-cd17-4818-8f9f-eb071e9cf189");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7803831f-5bbe-4b3c-b667-e904c587114d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b30f5234-d7ad-4e07-99c1-bad76dd00ac0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b539ec5d-735f-4328-ac1c-8b75f8492a8c");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { "1b3c5113-cd17-4818-8f9f-eb071e9cf189", "de1cbdae-358d-4ad8-a8f9-c7914f542fed", "Manager", "MANAGER" },
                    { "7803831f-5bbe-4b3c-b667-e904c587114d", "8976686f-f3ae-4385-94a9-edafbb66bb9e", "Admin", "ADMIN" },
                    { "b30f5234-d7ad-4e07-99c1-bad76dd00ac0", "e3965b43-2146-4086-a05f-07bd41ca26ce", "User", "USER" },
                    { "b539ec5d-735f-4328-ac1c-8b75f8492a8c", "7d2f29c1-4a19-4cc2-a322-9ec83a37b2dc", "SuperUser", "SUPERUSER" }
                });
        }
    }
}

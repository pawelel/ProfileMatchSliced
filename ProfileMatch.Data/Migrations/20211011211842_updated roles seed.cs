using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileMatch.Data.Migrations
{
    public partial class updatedrolesseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "057c9d7b-0a72-46a3-b8a8-e901c97679eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2673f49c-fcf2-4e2d-abd4-d96ff970d5fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5585c4b5-38d9-4d1d-8ec4-e96de0f025c3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ea2530a9-8bfe-4d38-8afa-c14c887df1eb", "dc64bb3f-2c2c-49e5-b413-901feacd9ce4", "User", "USER" },
                    { "eb2dd1fc-7e99-4e56-ab25-204b8d0180f6", "500310bf-48b6-475d-8bf0-7a8ebd89ade2", "Admin", "ADMIN" },
                    { "9e8b4fc0-5ffb-4263-aef2-eff60d3d854f", "21c00991-ab36-4452-afa4-c9916b34f415", "SuperUser", "SUPERUSER" },
                    { "8afa00a5-ed10-4dbd-9a2e-86e0101f84bb", "a229f1d5-38ba-46a2-94fd-d3e1a0cd0882", "Manager", "MANAGER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8afa00a5-ed10-4dbd-9a2e-86e0101f84bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e8b4fc0-5ffb-4263-aef2-eff60d3d854f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea2530a9-8bfe-4d38-8afa-c14c887df1eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb2dd1fc-7e99-4e56-ab25-204b8d0180f6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2673f49c-fcf2-4e2d-abd4-d96ff970d5fa", "310990f6-042e-4466-9b15-4b4d08ceb81a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "057c9d7b-0a72-46a3-b8a8-e901c97679eb", "5293dccb-7991-42f2-a656-79cac6b7a691", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5585c4b5-38d9-4d1d-8ec4-e96de0f025c3", "d8dfe942-5067-4992-b6b7-c7b1fec32374", "SuperUser", "SUPERUSER" });
        }
    }
}

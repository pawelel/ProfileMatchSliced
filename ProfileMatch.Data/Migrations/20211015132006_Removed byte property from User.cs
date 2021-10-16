using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileMatch.Data.Migrations
{
    public partial class RemovedbytepropertyfromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fbfa263-d84d-453a-9cfa-6b30cbfc1c6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6da20340-c249-462c-ae2a-c3756d8e6c99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2c58ce4-c696-4cf2-8952-8caed015590e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf8d3ec8-38a4-4c46-8741-434ad9b93aa7");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b30f5234-d7ad-4e07-99c1-bad76dd00ac0", "e3965b43-2146-4086-a05f-07bd41ca26ce", "User", "USER" },
                    { "7803831f-5bbe-4b3c-b667-e904c587114d", "8976686f-f3ae-4385-94a9-edafbb66bb9e", "Admin", "ADMIN" },
                    { "b539ec5d-735f-4328-ac1c-8b75f8492a8c", "7d2f29c1-4a19-4cc2-a322-9ec83a37b2dc", "SuperUser", "SUPERUSER" },
                    { "1b3c5113-cd17-4818-8f9f-eb071e9cf189", "de1cbdae-358d-4ad8-a8f9-c7914f542fed", "Manager", "MANAGER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6da20340-c249-462c-ae2a-c3756d8e6c99", "171abc0c-eece-47c0-a596-288a9b004bd3", "User", "USER" },
                    { "2fbfa263-d84d-453a-9cfa-6b30cbfc1c6c", "3650e566-e1da-4462-8385-8f12440e5fc3", "Admin", "ADMIN" },
                    { "cf8d3ec8-38a4-4c46-8741-434ad9b93aa7", "e10a85dc-4dc1-45a5-a75a-d1bbe1928792", "SuperUser", "SUPERUSER" },
                    { "c2c58ce4-c696-4cf2-8952-8caed015590e", "6c8cb58f-0360-4a31-a090-d6d6110e2cc6", "Manager", "MANAGER" }
                });
        }
    }
}
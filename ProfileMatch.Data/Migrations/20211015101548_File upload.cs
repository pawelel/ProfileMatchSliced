using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileMatch.Data.Migrations
{
    public partial class Fileupload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "071ec4cc-1e91-475a-b0a2-1ba079ca26d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93128dde-d1c7-47c9-b94e-01d6ef3b6890");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd313b3d-ba0f-4e63-83b0-6be303cc16e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0e6b18b-05bb-4fc0-9ca0-f076c6240c67");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "e0e6b18b-05bb-4fc0-9ca0-f076c6240c67", "cb719a7b-72ba-4bab-abbe-e746b6010ac8", "User", "USER" },
                    { "dd313b3d-ba0f-4e63-83b0-6be303cc16e9", "3ddce45f-213d-4f9c-aca8-146dcf201d55", "Admin", "ADMIN" },
                    { "071ec4cc-1e91-475a-b0a2-1ba079ca26d2", "723e4815-8972-40d3-8224-74a47141ba9d", "SuperUser", "SUPERUSER" },
                    { "93128dde-d1c7-47c9-b94e-01d6ef3b6890", "a495e21c-1194-4695-b487-c7f6564cfe21", "Manager", "MANAGER" }
                });
        }
    }
}
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileMatch.Data.Migrations
{
    public partial class Useranswernavigationproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0011b8f4-5254-4b17-be6e-d01190f9d033");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c488db9-56f7-4b5e-a312-d642378bed96");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c02e80d2-37fd-4203-bbd6-836a4c990aef");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f9d07c73-f13b-4db3-a9da-2f3a0eda14b2", "62f5f99a-74dd-4308-a987-29cec2b14dcf", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "139fc20e-4012-4222-8875-a210054fd777", "936c6410-3661-4be5-adef-d3d4582cc81a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c03b6c67-74fa-46d7-814a-8110abe61a74", "53e8e895-69c0-49b5-b14e-b5ec526520f8", "SuperUser", "SUPERUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "139fc20e-4012-4222-8875-a210054fd777");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c03b6c67-74fa-46d7-814a-8110abe61a74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9d07c73-f13b-4db3-a9da-2f3a0eda14b2");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Notes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0c488db9-56f7-4b5e-a312-d642378bed96", "e3b64a3b-fb2b-4fe9-b1c9-268f6db70abc", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c02e80d2-37fd-4203-bbd6-836a4c990aef", "dcd69154-e3a3-407b-ae4d-296d022234ac", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0011b8f4-5254-4b17-be6e-d01190f9d033", "e4abf71d-6fe7-41da-9317-c34789dbf4a6", "SuperUser", "SUPERUSER" });
        }
    }
}

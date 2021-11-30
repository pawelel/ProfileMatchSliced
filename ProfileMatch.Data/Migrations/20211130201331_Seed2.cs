using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e62fc78-9502-49ed-b313-688407884e75", new DateTime(2021, 11, 30, 21, 13, 31, 332, DateTimeKind.Local).AddTicks(8759), "AQAAAAEAACcQAAAAEJ7F97duVHIqHp1bvmGw28k0xYlsWK58ihqKVdteYwkMA7lj7km497wmWWxE5joEJQ==", "dc6ef2ee-d5b1-41ff-8753-ce5ccaf649a7" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, null, "HR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8744f75-de29-4386-a8fa-8b944d6379fc", new DateTime(2021, 11, 30, 20, 53, 58, 664, DateTimeKind.Local).AddTicks(966), "AQAAAAEAACcQAAAAEO2DSzZ+pSymfUIAzSxQleRijLZjl7U9vhMnn5hdjjvktjObMIljOSAGM4LFGPPzmw==", "b9aa8506-1114-443c-bb6b-c6e4dfce1343" });
        }
    }
}

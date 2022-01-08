using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Noanswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc7f6c07-2bf9-4847-ab11-e5fc1b6a2642", new DateTime(2022, 1, 8, 8, 46, 44, 917, DateTimeKind.Local).AddTicks(4010), "AQAAAAEAACcQAAAAEEOR2NCNlOyQGa14BF4BVcfqYWvukz08F4YsRy3x094jWrzqQhvvHtAC9rLeOPpDSg==", "91619ec8-fffc-424f-97fe-43cfb72cac2a" });

            migrationBuilder.UpdateData(
                table: "ClosedQuestions",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "ClosedQuestions",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsActive",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bea4d16-772d-46c1-88ca-1a047bb107f1", new DateTime(2022, 1, 8, 8, 39, 12, 580, DateTimeKind.Local).AddTicks(1999), "AQAAAAEAACcQAAAAEFhNzHQkpgysuE4waivUujr3CzEYbJ4xbsTNJSeE2b0gSQ8b9CVeGolWOimWU46x/w==", "3370dc60-bfac-4348-a340-34d392fcf9ef" });

            migrationBuilder.UpdateData(
                table: "ClosedQuestions",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "ClosedQuestions",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsActive",
                value: true);
        }
    }
}

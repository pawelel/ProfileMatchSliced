using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Identityuserroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7df59c27-21f9-4412-a79f-ed6a3176fc20", new DateTime(2021, 11, 23, 22, 16, 48, 126, DateTimeKind.Local).AddTicks(511), "AQAAAAEAACcQAAAAEJSWGZw7+AKyEqHY3Q2vjF9kDqwucsPVsBYp+Vp+wxSZRSzxkpXLDWa8iA5Wz2gdtg==", "fad8ec47-8dd9-474c-aa8a-e5c069b9f010" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00e8fd7b-3aac-494a-b896-c7d574e1f578", new DateTime(2021, 11, 23, 20, 35, 49, 473, DateTimeKind.Local).AddTicks(8589), "AQAAAAEAACcQAAAAEFpkufzfQgH+zaTFGfwnxnObo6W9KLW9GqTrbjdWQCtkdFYYO6mBhgDrfEaSkBSNlw==", "c840569b-5bc3-4897-8bec-f14e607e946d" });
        }
    }
}

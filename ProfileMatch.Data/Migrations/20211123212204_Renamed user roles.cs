using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Renameduserroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d433e5ae-25b8-4f3f-89b0-3bb7cab25eae", new DateTime(2021, 11, 23, 22, 22, 4, 204, DateTimeKind.Local).AddTicks(2677), "AQAAAAEAACcQAAAAEGUM+xHpEWu2GsYT/Zal0gx5vtyEBuxQJHnQm+qy/GVbFZLgjZ2kPLCqpsLR+gcFMw==", "06c54256-3aa7-4095-b35f-d9a166ea9847" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7df59c27-21f9-4412-a79f-ed6a3176fc20", new DateTime(2021, 11, 23, 22, 16, 48, 126, DateTimeKind.Local).AddTicks(511), "AQAAAAEAACcQAAAAEJSWGZw7+AKyEqHY3Q2vjF9kDqwucsPVsBYp+Vp+wxSZRSzxkpXLDWa8iA5Wz2gdtg==", "fad8ec47-8dd9-474c-aa8a-e5c069b9f010" });
        }
    }
}

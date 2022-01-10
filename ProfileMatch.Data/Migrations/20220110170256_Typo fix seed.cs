using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Typofixseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1eefd8b1-a7f3-4900-8d76-f38ec2b8b422", new DateTime(2022, 1, 10, 18, 2, 55, 693, DateTimeKind.Local).AddTicks(4149), "AQAAAAEAACcQAAAAEIYWeI+iQk+mAQbOrDbJ7K7e+mVfzJUGADcIU3G6sqLUi3PrZUjogP8+bpsSMF7MCQ==", "843ef28a-0821-4735-815b-10c6b2589224" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Software development");

            migrationBuilder.UpdateData(
                table: "OpenQuestions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NamePl" },
                values: new object[] { "What is important to me at work?", "Co jest dla mnie ważne w pracy?" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "952069b9-8d9a-4065-b45e-37afb86e5ee5", new DateTime(2022, 1, 10, 14, 38, 31, 6, DateTimeKind.Local).AddTicks(5911), "AQAAAAEAACcQAAAAEIAk8VvFE1JpED3Y+f/cpO43gk+BJLlosBFjry3ImPQGFb3j9agkbDTS0jsWuPDDzA==", "961840bc-a67b-4950-9a6c-ce9e113792ce" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Sftware development");

            migrationBuilder.UpdateData(
                table: "OpenQuestions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NamePl" },
                values: new object[] { "Co jest dla mnie ważne w pracy?", "What is important to me at work?" });
        }
    }
}

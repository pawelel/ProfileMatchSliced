using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Seed5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Level",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Level",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Level",
                value: 4);

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Level",
                value: 5);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be43918c-191e-47f3-8f0e-9aa39ad282a6", new DateTime(2021, 11, 30, 22, 49, 24, 631, DateTimeKind.Local).AddTicks(4736), "AQAAAAEAACcQAAAAEJbIVpTjOsZ21zDAwq7OurO4qbd48IJC0wKkVad2W0YXk8s0wR03ENvdV3wU4UYHqA==", "d2934390-1854-457a-9970-5b9268b456de" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Level",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Level",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Level",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Level",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07ecc66f-e0ee-4e97-bc9f-aa2b84b83c84", new DateTime(2021, 11, 30, 22, 43, 0, 331, DateTimeKind.Local).AddTicks(4895), "AQAAAAEAACcQAAAAEBJlMvCzhusf4+9ci4YPJlooaCmbxSMqg1oa80l8/NunlW/AFDxLXakkC7GvCOfrmg==", "91192a38-246c-477a-8acc-7a22974d240e" });
        }
    }
}

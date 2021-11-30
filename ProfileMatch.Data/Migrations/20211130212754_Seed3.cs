using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Seed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Nie znasz podstaw tego języka programowania");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9aaafb31-c9bf-4198-b415-af2ea197433d", new DateTime(2021, 11, 30, 22, 27, 53, 858, DateTimeKind.Local).AddTicks(2270), "AQAAAAEAACcQAAAAEC4WXkA2qVZ/DNaxF5MmUd/DJlsfWn81X9mEz6tJoXo8+fejOFE1sBcy7/Ec8cAVSA==", "87ac5ddf-1efe-411b-bcf9-9f7f0f825395" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Programowanie");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Sieci komputerowe");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 3, null, "Obsługa komputera" },
                    { 4, null, "Handel" }
                });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Jaka jest Twoja znajomość programowania?", "C#" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CategoryId", "Description", "IsActive", "Name" },
                values: new object[] { 2, 2, "Jaka jest Twoja znajomość sieci komputerowych?", true, "Konfiguracja routera" });

            migrationBuilder.InsertData(
                table: "AnswerOptions",
                columns: new[] { "Id", "Description", "Level", "QuestionId" },
                values: new object[] { 2, "Znasz podstawowe informacje na temat routera", 1, 2 });

            migrationBuilder.InsertData(
                table: "UserAnswers",
                columns: new[] { "ApplicationUserId", "QuestionId", "AnswerOptionId", "IsConfirmed", "LastModified", "SupervisorId" },
                values: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 2, 1, false, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserAnswers",
                keyColumns: new[] { "ApplicationUserId", "QuestionId" },
                keyValues: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 2 });

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Hello world");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e62fc78-9502-49ed-b313-688407884e75", new DateTime(2021, 11, 30, 21, 13, 31, 332, DateTimeKind.Local).AddTicks(8759), "AQAAAAEAACcQAAAAEJ7F97duVHIqHp1bvmGw28k0xYlsWK58ihqKVdteYwkMA7lj7km497wmWWxE5joEJQ==", "dc6ef2ee-d5b1-41ff-8753-ce5ccaf649a7" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pieluchowanie");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "czy potrafisz otworzyć pieluchę?", "Wymiana pieluchy" });
        }
    }
}

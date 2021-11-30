using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Seed4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "QuestionId" },
                values: new object[] { "Znasz podstawowe rzeczy związane z programowaniem w C#", 1 });

            migrationBuilder.InsertData(
                table: "AnswerOptions",
                columns: new[] { "Id", "Description", "Level", "QuestionId" },
                values: new object[,]
                {
                    { 3, "Potrafisz pisać proste kody w języku", 1, 1 },
                    { 4, "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)", 1, 1 },
                    { 5, "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw", 1, 1 },
                    { 6, "Znasz podstawowe informacje na temat routera", 1, 2 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07ecc66f-e0ee-4e97-bc9f-aa2b84b83c84", new DateTime(2021, 11, 30, 22, 43, 0, 331, DateTimeKind.Local).AddTicks(4895), "AQAAAAEAACcQAAAAEBJlMvCzhusf4+9ci4YPJlooaCmbxSMqg1oa80l8/NunlW/AFDxLXakkC7GvCOfrmg==", "91192a38-246c-477a-8acc-7a22974d240e" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, null, "Lingwistyka" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Jaka jest Twoja znajomość programowania w C#?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 1, "Jaka jest Twoja znajomość programowania w C++?", "C++" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CategoryId", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 3, 1, "Jaka jest Twoja znajomość programowania w Pythonie?", true, "Python" },
                    { 4, 2, "Jaka jest Twoja znajomość sieci komputerowych?", true, "Konfiguracja routera" }
                });

            migrationBuilder.UpdateData(
                table: "UserAnswers",
                keyColumns: new[] { "ApplicationUserId", "QuestionId" },
                keyValues: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 1 },
                column: "AnswerOptionId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "QuestionId" },
                values: new object[] { "Znasz podstawowe informacje na temat routera", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9aaafb31-c9bf-4198-b415-af2ea197433d", new DateTime(2021, 11, 30, 22, 27, 53, 858, DateTimeKind.Local).AddTicks(2270), "AQAAAAEAACcQAAAAEC4WXkA2qVZ/DNaxF5MmUd/DJlsfWn81X9mEz6tJoXo8+fejOFE1sBcy7/Ec8cAVSA==", "87ac5ddf-1efe-411b-bcf9-9f7f0f825395" });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Jaka jest Twoja znajomość programowania?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 2, "Jaka jest Twoja znajomość sieci komputerowych?", "Konfiguracja routera" });

            migrationBuilder.UpdateData(
                table: "UserAnswers",
                keyColumns: new[] { "ApplicationUserId", "QuestionId" },
                keyValues: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 1 },
                column: "AnswerOptionId",
                value: 1);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Seed6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AnswerOptions",
                columns: new[] { "Id", "Description", "Level", "QuestionId" },
                values: new object[,]
                {
                    { 7, "Potrafisz zalogować się do routera i swobodnie poruszasz się po interfejsie", 2, 2 },
                    { 8, "Potrafisz skonfigurować podstawowe ustawienia sieciowe w routerze", 3, 2 },
                    { 9, "Potrafisz skonfigurować router dla wielu urządzeń oraz zadbać o bezpieczeństwo w sieci", 4, 2 },
                    { 10, "Potrafisz skonfigurować router w systemie linux w trybie tekstowym", 5, 2 },
                    { 11, "Nie konfigurowałeś żadnej usługi Active Directory", 1, 3 },
                    { 12, "Instalowałeś usługę Active Directory, ale jej nie konfigurowałeś", 2, 3 },
                    { 13, "Potrafisz dodawać podstawowe usługi do domeny i zrobić prostą konfiguracje", 3, 3 },
                    { 14, "Łatwość sprawia ci surfowanie po ustawieniach sieciowych domeny, bez problemu radzisz sobie z tworzeniem domen i dodawaniem kont użytkowników lub grup", 4, 3 },
                    { 15, "Usługa AD jest dla ciebie chlebem powszednim i nie sprawia ci żadnych problemów", 5, 3 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72604d7e-00cf-46db-924e-21222358c120", new DateTime(2021, 11, 30, 23, 3, 18, 455, DateTimeKind.Local).AddTicks(7367), "AQAAAAEAACcQAAAAEP+inWIMM7ouvTQH3/HSV7nciZ6AtAPOdoEbo2q5JBhCaj8VTEQbKFIByKgPfs9aMA==", "59683dcf-32cc-4147-9516-6e720d12b6da" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CategoryId", "Description", "IsActive", "Name" },
                values: new object[] { 5, 2, "Jaka jest Twoja znajomość usługi Active Directory?", true, "Usługa Active Directory" });

            migrationBuilder.UpdateData(
                table: "UserAnswers",
                keyColumns: new[] { "ApplicationUserId", "QuestionId" },
                keyValues: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 2 },
                column: "AnswerOptionId",
                value: 4);

            migrationBuilder.InsertData(
                table: "UserAnswers",
                columns: new[] { "ApplicationUserId", "QuestionId", "AnswerOptionId", "IsConfirmed", "LastModified", "SupervisorId" },
                values: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 3, 3, false, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserAnswers",
                keyColumns: new[] { "ApplicationUserId", "QuestionId" },
                keyValues: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be43918c-191e-47f3-8f0e-9aa39ad282a6", new DateTime(2021, 11, 30, 22, 49, 24, 631, DateTimeKind.Local).AddTicks(4736), "AQAAAAEAACcQAAAAEJbIVpTjOsZ21zDAwq7OurO4qbd48IJC0wKkVad2W0YXk8s0wR03ENvdV3wU4UYHqA==", "d2934390-1854-457a-9970-5b9268b456de" });

            migrationBuilder.UpdateData(
                table: "UserAnswers",
                keyColumns: new[] { "ApplicationUserId", "QuestionId" },
                keyValues: new object[] { "a96d7c75-47f4-409b-a4d1-03f93c105647", 2 },
                column: "AnswerOptionId",
                value: 1);
        }
    }
}

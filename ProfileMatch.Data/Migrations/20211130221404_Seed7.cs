using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Seed7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Nie znasz podstaw tego języka programowania");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "Znasz podstawowe rzeczy związane z programowaniem w C++");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Potrafisz pisać proste kody w języku");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "Nie znasz podstaw tego języka programowania");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "Znasz podstawowe rzeczy związane z programowaniem w Pythonie");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "Potrafisz pisać proste kody w języku");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "Potrafisz pisać kod, który jest bardziej zaawansowany (wiesz na czym polegają warunki, pętle, obiekty, funkcje)");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "Bez problemu analizujesz kod, edytujesz go, wprowadzasz nowe zmiany lub piszesz program od podstaw");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b38e6726-7782-4db8-9528-20aaecc6f269", new DateTime(2021, 11, 30, 23, 14, 3, 699, DateTimeKind.Local).AddTicks(1668), "AQAAAAEAACcQAAAAEBx7wgjO8h27hM5ua9ujgbhvP1I78KnzZuF9IEjlM9kpmwdnRtcFRovuhZ60WFEoTA==", "1b5fb9a0-ede8-45dc-a8e1-22912a742909" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Znasz podstawowe informacje na temat routera");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "Potrafisz zalogować się do routera i swobodnie poruszasz się po interfejsie");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "Potrafisz skonfigurować podstawowe ustawienia sieciowe w routerze");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Potrafisz skonfigurować router dla wielu urządzeń oraz zadbać o bezpieczeństwo w sieci");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "Potrafisz skonfigurować router w systemie linux w trybie tekstowym");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "Nie konfigurowałeś żadnej usługi Active Directory");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "Instalowałeś usługę Active Directory, ale jej nie konfigurowałeś");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "Potrafisz dodawać podstawowe usługi do domeny i zrobić prostą konfiguracje");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "Łatwość sprawia ci surfowanie po ustawieniach sieciowych domeny, bez problemu radzisz sobie z tworzeniem domen i dodawaniem kont użytkowników lub grup");

            migrationBuilder.UpdateData(
                table: "AnswerOptions",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "Usługa AD jest dla ciebie chlebem powszednim i nie sprawia ci żadnych problemów");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72604d7e-00cf-46db-924e-21222358c120", new DateTime(2021, 11, 30, 23, 3, 18, 455, DateTimeKind.Local).AddTicks(7367), "AQAAAAEAACcQAAAAEP+inWIMM7ouvTQH3/HSV7nciZ6AtAPOdoEbo2q5JBhCaj8VTEQbKFIByKgPfs9aMA==", "59683dcf-32cc-4147-9516-6e720d12b6da" });
        }
    }
}

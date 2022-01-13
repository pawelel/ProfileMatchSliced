using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Removeroleuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93877c5b-c988-4c83-b152-d0b17858f7c6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5698e531-21d1-4f87-8ba7-5263852cd1a9", new DateTime(2022, 1, 13, 11, 0, 28, 624, DateTimeKind.Local).AddTicks(9241), "AQAAAAEAACcQAAAAEJZbnTvKoga3Mj4avMNAV+DgieD/ygjr9Duw6ViWbuwCeJpTnp+EhHM927onSMDuJw==", "d9bdde03-bf34-4619-8f2e-97ea88e68078" });

            migrationBuilder.UpdateData(
                table: "ClosedQuestions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "How good are you in Computer Networks?");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93877c5b-c988-4c83-b152-d0b17858f7c6", "dd434162-84c9-4f3f-acd0-aa028df9b1f4", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa1c7149-fc92-4778-a2c2-5ccb5ba4d9bc", new DateTime(2022, 1, 11, 6, 59, 50, 64, DateTimeKind.Local).AddTicks(7682), "AQAAAAEAACcQAAAAEMpIFzuy4bIAr7JLSXPOu8b+Q65Zb5in0dbmvWnhU3xqaQdBVLZ67okFGnTeCMLTTQ==", "2eca0ef1-f0f0-486f-b14c-f7fce38a17c9" });

            migrationBuilder.UpdateData(
                table: "ClosedQuestions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "How good are you in CComputer Networks?");
        }
    }
}

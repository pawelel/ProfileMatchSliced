using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileMatch.Data.Migrations
{
    public partial class Useransweraddlastmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "139fc20e-4012-4222-8875-a210054fd777");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c03b6c67-74fa-46d7-814a-8110abe61a74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9d07c73-f13b-4db3-a9da-2f3a0eda14b2");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "UserAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d1dff24-8e71-4617-9d60-ed54b53e6119", "62b94438-152b-470e-84b4-25e0a5f803d9", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc3781ac-1815-4ca6-bb44-2019ed905885", "fa150426-e573-4a74-b855-be1206516194", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "68c8e6d5-aee0-4d22-8b78-e7206fd39628", "644f4d3d-548c-42d3-9b79-5ca631701af0", "SuperUser", "SUPERUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d1dff24-8e71-4617-9d60-ed54b53e6119");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68c8e6d5-aee0-4d22-8b78-e7206fd39628");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3781ac-1815-4ca6-bb44-2019ed905885");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "UserAnswers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f9d07c73-f13b-4db3-a9da-2f3a0eda14b2", "62f5f99a-74dd-4308-a987-29cec2b14dcf", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "139fc20e-4012-4222-8875-a210054fd777", "936c6410-3661-4be5-adef-d3d4582cc81a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c03b6c67-74fa-46d7-814a-8110abe61a74", "53e8e895-69c0-49b5-b14e-b5ec526520f8", "SuperUser", "SUPERUSER" });
        }
    }
}

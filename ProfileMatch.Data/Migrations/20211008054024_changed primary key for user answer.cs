using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileMatch.Data.Migrations
{
    public partial class Changedprimarykeyforuseranswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72cdc868-e2eb-42a4-8302-7a9dd24c082d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1b6953e-78cd-4a33-83fd-3637da52c767");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cee4dad4-6726-4c35-af81-086a5494b9cc");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerOptionId",
                table: "UserAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers",
                columns: new[] { "ApplicationUserId", "QuestionId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e005efd-24fc-45da-8097-fd35ce6d61e5", "c9a84a65-f784-44c6-9f66-d3aafeeb2e93", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe9f31f1-b132-44b0-ac14-52bff4cab565", "8ce0437d-77c8-41c9-a979-089820723df8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5c2d5cdc-bd2b-4d9e-984a-4ec059d782c4", "45450e2e-04b6-4423-b247-4a7cbf8e66c7", "SuperUser", "SUPERUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c2d5cdc-bd2b-4d9e-984a-4ec059d782c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e005efd-24fc-45da-8097-fd35ce6d61e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe9f31f1-b132-44b0-ac14-52bff4cab565");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerOptionId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers",
                columns: new[] { "ApplicationUserId", "AnswerOptionId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "72cdc868-e2eb-42a4-8302-7a9dd24c082d", "88c07ab3-a2c6-4017-92c2-fc3e304dec22", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b1b6953e-78cd-4a33-83fd-3637da52c767", "9da234ab-b761-46ef-b3c3-1098fbaa1e9e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cee4dad4-6726-4c35-af81-086a5494b9cc", "4f577209-f5b2-4960-ac6d-6450e37806b0", "SuperUser", "SUPERUSER" });
        }
    }
}

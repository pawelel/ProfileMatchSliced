using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Roleaddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9588cfdb-8071-49c0-82cf-c51f20d305d2", "83e0991b-0ddb-4291-bfe6-f9217019fde5", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "400ea799-646b-4bcd-9b57-d58c120d716d", "AQAAAAEAACcQAAAAEH40x4SD2znQ7dXLw5aqTl7aDD9Tb96BLUbhvG+Y+Ezdp1RtOq0QyECq5JwoxkdtSg==", "ffba8eeb-b2f2-44fd-9130-125a30a2f1f3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9588cfdb-8071-49c0-82cf-c51f20d305d2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81aaa505-5780-4e18-9510-49a1bf892fc2", "AQAAAAEAACcQAAAAEGwBDrCZgZlhSjBCFckhUCIh42Gc+Ug5cYJErK2uVCmDGx1QUSknlQtJ7zuCCVowYw==", "79fd9bbf-dd6f-4677-a678-4ba7775228cf" });
        }
    }
}

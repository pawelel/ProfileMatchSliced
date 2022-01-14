using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class Certificateupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "Certificates",
                newName: "ValidToDate");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionPl",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhotoPath", "SecurityStamp" },
                values: new object[] { "81aaa505-5780-4e18-9510-49a1bf892fc2", "AQAAAAEAACcQAAAAEGwBDrCZgZlhSjBCFckhUCIh42Gc+Ug5cYJErK2uVCmDGx1QUSknlQtJ7zuCCVowYw==", "/blank-profile.png", "79fd9bbf-dd6f-4677-a678-4ba7775228cf" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionPl",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "ValidToDate",
                table: "Certificates",
                newName: "DateModified");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhotoPath", "SecurityStamp" },
                values: new object[] { "30b66b59-447d-4c20-acfd-c41f8fce6571", "AQAAAAEAACcQAAAAEA7gpniRqOhGbX3OVv1plgKhHVDGYMZQO57xAm3KT+mbdanTTBIMNLMrFesdgtnefA==", "/lank-profile.png", "bde9c84c-0e35-4ced-a804-2ce4440b0f2c" });
        }
    }
}

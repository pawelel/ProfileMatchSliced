using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class ImageThumb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Certificates",
                newName: "ImageThumb");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5283cbc-1e0d-448a-b0b9-3b27183facde", "AQAAAAEAACcQAAAAEKcFT2/tM/PQbN87YYoV2AdJRaUtQLpil++yAsQv2oFMQgm+RJ0hmYe3fTHn7KPjEg==", "6c17aef9-6e46-440b-8e1c-a85a74a16150" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "ImageThumb",
                table: "Certificates",
                newName: "Image");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe2ec594-0be8-4b7f-b6b6-dac62d677209", "AQAAAAEAACcQAAAAEBlOjsS1mYAm/18gRHFZo/Q+HRd3lUFyXNaZsDABAmPWTkRUg6Hq1Ht9z7vLMKJCng==", "62604232-2d84-43e0-952e-1bf620df24eb" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class RemoveunnecessarystringsfromCert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageThumb",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "PdfPath",
                table: "Certificates");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a96d7c75-47f4-409b-a4d1-03f93c105647",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe1e7003-5535-4569-8b19-66622f59f73b", "AQAAAAEAACcQAAAAELmQhGYdpt9U1cG+adqBmaKAsXjJi70sdd1lcPVlbXguB/Rsk7ObCgdMfi95wEtdlA==", "2f37dc7e-2a79-4f29-b681-93f7a6672dcc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageThumb",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfPath",
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
    }
}

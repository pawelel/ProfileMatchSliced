using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileMatch.Data.Migrations
{
    public partial class Current : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6608d5a3-6e5d-4f43-9728-7c32fc2677e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5f06608-e8c9-441f-805f-9f7420566d22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc64e327-b71a-4c1b-a759-7160aca6844b");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "UserAnswers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0c488db9-56f7-4b5e-a312-d642378bed96", "e3b64a3b-fb2b-4fe9-b1c9-268f6db70abc", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c02e80d2-37fd-4203-bbd6-836a4c990aef", "dcd69154-e3a3-407b-ae4d-296d022234ac", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0011b8f4-5254-4b17-be6e-d01190f9d033", "e4abf71d-6fe7-41da-9317-c34789dbf4a6", "SuperUser", "SUPERUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0011b8f4-5254-4b17-be6e-d01190f9d033");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c488db9-56f7-4b5e-a312-d642378bed96");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c02e80d2-37fd-4203-bbd6-836a4c990aef");

            migrationBuilder.AlterColumn<int>(
                name: "SupervisorId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6608d5a3-6e5d-4f43-9728-7c32fc2677e9", "dd121fbb-b8dd-42e9-bd59-5c26098149b5", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fc64e327-b71a-4c1b-a759-7160aca6844b", "7043ae79-e83a-4d0b-9ea7-70b421f7beb1", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d5f06608-e8c9-441f-805f-9f7420566d22", "e2f05444-cf3b-4150-801f-a7cd32c21fc2", "SuperUser", "SUPERUSER" });
        }
    }
}

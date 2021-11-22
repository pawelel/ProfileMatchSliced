using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileMatch.Data.Migrations
{
    public partial class UsernoteIsdisplayed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10a3d590-a0d8-4959-8db8-fca04b5632fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "560b6a16-a6bc-4050-a21c-8f7ceb49cfa7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d759a464-69e4-4b8d-a817-631073c8b560");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisplayed",
                table: "UserNotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83956e58-0f7e-4110-a08e-c42083738b2c", "aeb5035f-abd4-4c68-a408-be8f4c688cd0", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c45ce426-5734-46eb-9ea8-63d74bb9a2b3", "e14e96cd-7ce7-4f96-9b6e-7f23801fb0e4", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "de139a33-7763-47d3-81c1-d3e0c03c5222", "e9fc1e52-de85-43bd-9b8d-abfc72247139", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83956e58-0f7e-4110-a08e-c42083738b2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c45ce426-5734-46eb-9ea8-63d74bb9a2b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de139a33-7763-47d3-81c1-d3e0c03c5222");

            migrationBuilder.DropColumn(
                name: "IsDisplayed",
                table: "UserNotes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10a3d590-a0d8-4959-8db8-fca04b5632fd", "aa3b47c5-602d-48f0-86a2-c72db5530198", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "560b6a16-a6bc-4050-a21c-8f7ceb49cfa7", "b4f9f9e9-6677-4cab-892e-5c8be5a4eff5", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d759a464-69e4-4b8d-a817-631073c8b560", "1c8f2f55-22a1-4b81-8ab3-4e487dfa8f86", "Admin", "ADMIN" });
        }
    }
}

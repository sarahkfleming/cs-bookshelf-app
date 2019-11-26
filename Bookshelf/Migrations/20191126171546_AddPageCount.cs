using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookshelf.Migrations
{
    public partial class AddPageCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PageCount",
                table: "Books",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c329e554-39cd-493d-bcb5-9e4c3eb1a029", "AQAAAAEAACcQAAAAEBNizZo3p0s/Hr6bpfyUTKE7aV1tYqkeay4+zLnU/lPF4cIgwOf8HeH7hQp8Z8xetQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageCount",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a06807db-180a-4e93-a7df-2d02bcb8ad13", "AQAAAAEAACcQAAAAENfGa7DZwx4Nr1qZXtBn/eh4RR1hHG6MqfnRcNAVW7mTWXNJrmmreMwbh/QPcF6rYw==" });
        }
    }
}

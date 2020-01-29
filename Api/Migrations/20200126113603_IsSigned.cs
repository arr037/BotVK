using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class IsSigned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSignedGroup",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSignedGroup",
                table: "Users");
        }
    }
}

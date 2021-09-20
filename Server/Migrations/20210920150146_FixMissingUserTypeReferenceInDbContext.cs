using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class FixMissingUserTypeReferenceInDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoolName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoolName",
                table: "AspNetUsers");
        }
    }
}

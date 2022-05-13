using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTrackr.API.Persistence.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "tb_Package");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "tb_Package",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

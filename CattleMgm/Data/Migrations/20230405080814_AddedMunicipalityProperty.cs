using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CattleMgm.Data.Migrations
{
    public partial class AddedMunicipalityProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Municipality",
                table: "AspNetUsers");
        }
    }
}

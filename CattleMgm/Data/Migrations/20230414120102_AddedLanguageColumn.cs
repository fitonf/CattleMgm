using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CattleMgm.Data.Migrations
{
    public partial class AddedLanguageColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateTable(
            //    name: "ListOfMenus",
            //    columns: table => new
            //    {
            //        MenuId = table.Column<int>(type: "int", nullable: false),
            //        MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MenuArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MenuController = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MenuAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MenuStaysOpenFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SubMenuId = table.Column<int>(type: "int", nullable: true),
            //        SubMenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SubMenuArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SubMenuController = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SubMenuAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SubMenuStaysOpenFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SubIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        HasSub = table.Column<bool>(type: "bit", nullable: false),
            //        OrdinalNumberM = table.Column<int>(type: "int", nullable: false),
            //        OrdinalNumberS = table.Column<int>(type: "int", nullable: false),
            //        IsBlazor = table.Column<bool>(type: "bit", nullable: false),
            //        ParentId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ListOfMenusAccess",
            //    columns: table => new
            //    {
            //        MenuId = table.Column<int>(type: "int", nullable: false),
            //        submenu = table.Column<int>(type: "int", nullable: false),
            //        MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SubmenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        HasSub = table.Column<bool>(type: "bit", nullable: false),
            //        HasAccess = table.Column<bool>(type: "bit", nullable: false),
            //        policy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ListOfMenus");

            //migrationBuilder.DropTable(
            //    name: "ListOfMenusAccess");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "AspNetUsers");
        }
    }
}

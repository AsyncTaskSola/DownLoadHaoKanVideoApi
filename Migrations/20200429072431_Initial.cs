using Microsoft.EntityFrameworkCore.Migrations;

namespace DownLoadHaoKanVideoAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emplyees",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    Gender = table.Column<sbyte>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Status = table.Column<sbyte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emplyees", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emplyees");
        }
    }
}

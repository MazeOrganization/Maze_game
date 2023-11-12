using Microsoft.EntityFrameworkCore.Migrations;

namespace Mazes.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maze",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(60)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    BoardData = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maze", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maze");
        }
    }
}

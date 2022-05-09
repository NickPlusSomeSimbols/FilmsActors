using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsActors.Data.Migrations
{
    public partial class initialmigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsRated",
                table: "Film",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsRated",
                table: "Actor",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRated",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "IsRated",
                table: "Actor");
        }
    }
}

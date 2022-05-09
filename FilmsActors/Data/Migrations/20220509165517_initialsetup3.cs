using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsActors.Data.Migrations
{
    public partial class initialsetup3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NegRated",
                table: "Film",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PosRated",
                table: "Film",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NegRated",
                table: "Actor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PosRated",
                table: "Actor",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NegRated",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "PosRated",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "NegRated",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "PosRated",
                table: "Actor");
        }
    }
}

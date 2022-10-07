using Microsoft.EntityFrameworkCore.Migrations;

namespace CSI_PPMS.Migrations
{
    public partial class temprowchecks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "disc_marking",
                table: "table_rows",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "shell_marking",
                table: "table_rows",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "disc_marking",
                table: "table_rows");

            migrationBuilder.DropColumn(
                name: "shell_marking",
                table: "table_rows");
        }
    }
}

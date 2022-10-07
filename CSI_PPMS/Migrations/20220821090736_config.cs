using Microsoft.EntityFrameworkCore.Migrations;

namespace CSI_PPMS.Migrations
{
    public partial class config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PLC_slot",
                table: "tcp_config",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "plc_rack",
                table: "tcp_config",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "technifor_rack",
                table: "tcp_config",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "technifor_slot",
                table: "tcp_config",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PLC_slot",
                table: "tcp_config");

            migrationBuilder.DropColumn(
                name: "plc_rack",
                table: "tcp_config");

            migrationBuilder.DropColumn(
                name: "technifor_rack",
                table: "tcp_config");

            migrationBuilder.DropColumn(
                name: "technifor_slot",
                table: "tcp_config");
        }
    }
}

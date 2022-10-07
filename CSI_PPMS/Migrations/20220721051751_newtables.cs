using Microsoft.EntityFrameworkCore.Migrations;

namespace CSI_PPMS.Migrations
{
    public partial class newtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "marking_status_ack",
                table: "punching_cycle_status",
                newName: "Marking_Status_Ack");

            migrationBuilder.RenameColumn(
                name: "data_request_ack",
                table: "punching_cycle_status",
                newName: "Data_Request_Ack");

            migrationBuilder.AddColumn<long>(
                name: "Marking_Complete_Ack",
                table: "punching_cycle_status",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "PLC_MODE",
                table: "punching_cycle_status",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Start_Punching",
                table: "punching_cycle_status",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marking_Complete_Ack",
                table: "punching_cycle_status");

            migrationBuilder.DropColumn(
                name: "PLC_MODE",
                table: "punching_cycle_status");

            migrationBuilder.DropColumn(
                name: "Start_Punching",
                table: "punching_cycle_status");

            migrationBuilder.RenameColumn(
                name: "Marking_Status_Ack",
                table: "punching_cycle_status",
                newName: "marking_status_ack");

            migrationBuilder.RenameColumn(
                name: "Data_Request_Ack",
                table: "punching_cycle_status",
                newName: "data_request_ack");
        }
    }
}

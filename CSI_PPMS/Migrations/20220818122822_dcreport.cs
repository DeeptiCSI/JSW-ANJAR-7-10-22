using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSI_PPMS.Migrations
{
    public partial class dcreport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "down_coiler_reports_data",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plate_id = table.Column<long>(type: "bigint", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    disc_line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disc_line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shell_line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shell_line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shell_line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shell_line4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    logo_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coil_width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coil_dismeter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mat_id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_down_coiler_reports_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_down_coiler_reports_data_plate_info_from_sap_plate_id",
                        column: x => x.plate_id,
                        principalTable: "plate_info_from_sap",
                        principalColumn: "plate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_down_coiler_reports_data_plate_id",
                table: "down_coiler_reports_data",
                column: "plate_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "down_coiler_reports_data");
        }
    }
}

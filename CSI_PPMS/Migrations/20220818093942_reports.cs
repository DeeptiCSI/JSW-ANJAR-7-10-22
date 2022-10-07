using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSI_PPMS.Migrations
{
    public partial class reports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ys_data_records",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plate_id = table.Column<long>(type: "bigint", nullable: false),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ys_value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ys_data_records", x => x.id);
                    table.ForeignKey(
                        name: "FK_ys_data_records_plate_data_from_sap_cold_leveller_plate_id",
                        column: x => x.plate_id,
                        principalTable: "plate_data_from_sap_cold_leveller",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ys_data_records_plate_id",
                table: "ys_data_records",
                column: "plate_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ys_data_records");
        }
    }
}

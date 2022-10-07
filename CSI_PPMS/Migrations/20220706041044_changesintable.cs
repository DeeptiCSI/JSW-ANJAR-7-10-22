using Microsoft.EntityFrameworkCore.Migrations;

namespace CSI_PPMS.Migrations
{
    public partial class changesintable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_down_coiler_records_plate_info_from_sap_plate_id",
                table: "down_coiler_records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_down_coiler_records",
                table: "down_coiler_records");

            migrationBuilder.RenameTable(
                name: "down_coiler_records",
                newName: "cold_leveller_records");

            migrationBuilder.RenameIndex(
                name: "IX_down_coiler_records_plate_id",
                table: "cold_leveller_records",
                newName: "IX_cold_leveller_records_plate_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cold_leveller_records",
                table: "cold_leveller_records",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_cold_leveller_records_plate_data_from_sap_cold_leveller_plate_id",
                table: "cold_leveller_records",
                column: "plate_id",
                principalTable: "plate_data_from_sap_cold_leveller",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cold_leveller_records_plate_data_from_sap_cold_leveller_plate_id",
                table: "cold_leveller_records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cold_leveller_records",
                table: "cold_leveller_records");

            migrationBuilder.RenameTable(
                name: "cold_leveller_records",
                newName: "down_coiler_records");

            migrationBuilder.RenameIndex(
                name: "IX_cold_leveller_records_plate_id",
                table: "down_coiler_records",
                newName: "IX_down_coiler_records_plate_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_down_coiler_records",
                table: "down_coiler_records",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_down_coiler_records_plate_info_from_sap_plate_id",
                table: "down_coiler_records",
                column: "plate_id",
                principalTable: "plate_info_from_sap",
                principalColumn: "plate_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

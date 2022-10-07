using Microsoft.EntityFrameworkCore.Migrations;

namespace CSI_PPMS.Migrations
{
    public partial class newColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_logs_module_module_id",
                table: "app_logs");

            migrationBuilder.AddColumn<long>(
                name: "type",
                table: "sap_credentials",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "module_id",
                table: "app_logs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_app_logs_module_module_id",
                table: "app_logs",
                column: "module_id",
                principalTable: "module",
                principalColumn: "module_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_logs_module_module_id",
                table: "app_logs");

            migrationBuilder.DropColumn(
                name: "type",
                table: "sap_credentials");

            migrationBuilder.AlterColumn<long>(
                name: "module_id",
                table: "app_logs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_app_logs_module_module_id",
                table: "app_logs",
                column: "module_id",
                principalTable: "module",
                principalColumn: "module_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

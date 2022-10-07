using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSI_PPMS.Migrations
{
    public partial class newTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "template_master",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_id = table.Column<long>(type: "bigint", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_master", x => x.id);
                    table.ForeignKey(
                        name: "FK_template_master_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_template_master_user_created_by",
                        column: x => x.created_by,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_rows",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    template_master_id = table.Column<long>(type: "bigint", nullable: false),
                    row = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_rows", x => x.id);
                    table.ForeignKey(
                        name: "FK_table_rows_template_master_template_master_id",
                        column: x => x.template_master_id,
                        principalTable: "template_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_rows_template_master_id",
                table: "table_rows",
                column: "template_master_id");

            migrationBuilder.CreateIndex(
                name: "IX_template_master_created_by",
                table: "template_master",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_template_master_module_id",
                table: "template_master",
                column: "module_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "table_rows");

            migrationBuilder.DropTable(
                name: "template_master");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSI_PPMS.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "check_box_table",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_marked = table.Column<bool>(type: "bit", nullable: false),
                    is_punched = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_check_box_table", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "module",
                columns: table => new
                {
                    module_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module", x => x.module_id);
                });

            migrationBuilder.CreateTable(
                name: "punching_cycle_status",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    marking_status_ack = table.Column<long>(type: "bigint", nullable: false),
                    data_request_ack = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_punching_cycle_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "plate_marking_data_for_receipe",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plate_id = table.Column<long>(type: "bigint", nullable: false),
                    module_id = table.Column<long>(type: "bigint", nullable: false),
                    plate_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    marking_position = table.Column<long>(type: "bigint", nullable: false),
                    plc_ack = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plate_marking_data_for_receipe", x => x.id);
                    table.ForeignKey(
                        name: "FK_plate_marking_data_for_receipe_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plate_punching_data_for_receipe",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plate_id = table.Column<long>(type: "bigint", nullable: false),
                    module_id = table.Column<long>(type: "bigint", nullable: false),
                    plate_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    plc_ack = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plate_punching_data_for_receipe", x => x.id);
                    table.ForeignKey(
                        name: "FK_plate_punching_data_for_receipe_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sap_credentials",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sap_link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sap_username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sap_password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    module_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sap_credentials", x => x.id);
                    table.ForeignKey(
                        name: "FK_sap_credentials_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "plate_data_from_sap_cold_leveller",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plate_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thickness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ys_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    return1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    module_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plate_data_from_sap_cold_leveller", x => x.id);
                    table.ForeignKey(
                        name: "FK_plate_data_from_sap_cold_leveller_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_plate_data_from_sap_cold_leveller_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plate_info_from_sap",
                columns: table => new
                {
                    plate_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    plate_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    heat_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thickness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    purchase_order = table.Column<long>(type: "bigint", nullable: true),
                    purchase_order_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    material_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    project_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weighing_mode = table.Column<int>(type: "int", nullable: true),
                    theoretical_weight = table.Column<bool>(type: "bit", nullable: true),
                    actual_weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    grade_duel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    charecter_count = table.Column<int>(type: "int", nullable: true),
                    data_from_sap_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    weight_read_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sent_to_punching_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sent_to_marking_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_to_sap_time = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plate_info_from_sap", x => x.plate_id);
                    table.ForeignKey(
                        name: "FK_plate_info_from_sap_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_plate_info_from_sap_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tcp_config",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    technifor_ip_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plc_ip_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    technifor_port_no = table.Column<int>(type: "int", nullable: false),
                    plc_port_no = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    module_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tcp_config", x => x.id);
                    table.ForeignKey(
                        name: "FK_tcp_config_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tcp_config_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    user_role_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    module_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => x.user_role_id);
                    table.ForeignKey(
                        name: "FK_user_role_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "down_coiler_records",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    plate_id = table.Column<long>(type: "bigint", nullable: false),
                    plate_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    steel_grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thickness = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_down_coiler_records", x => x.id);
                    table.ForeignKey(
                        name: "FK_down_coiler_records_plate_info_from_sap_plate_id",
                        column: x => x.plate_id,
                        principalTable: "plate_info_from_sap",
                        principalColumn: "plate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plate_marking_record",
                columns: table => new
                {
                    marking_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_id = table.Column<long>(type: "bigint", nullable: true),
                    plate_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plate_id = table.Column<long>(type: "bigint", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line6 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plate_marking_record", x => x.marking_id);
                    table.ForeignKey(
                        name: "FK_plate_marking_record_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_plate_marking_record_plate_info_from_sap_plate_id",
                        column: x => x.plate_id,
                        principalTable: "plate_info_from_sap",
                        principalColumn: "plate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plate_punching_record",
                columns: table => new
                {
                    Punching_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_id = table.Column<long>(type: "bigint", nullable: true),
                    plate_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plate_id = table.Column<long>(type: "bigint", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line6 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plate_punching_record", x => x.Punching_id);
                    table.ForeignKey(
                        name: "FK_plate_punching_record_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_plate_punching_record_plate_info_from_sap_plate_id",
                        column: x => x.plate_id,
                        principalTable: "plate_info_from_sap",
                        principalColumn: "plate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_down_coiler_records_plate_id",
                table: "down_coiler_records",
                column: "plate_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_data_from_sap_cold_leveller_module_id",
                table: "plate_data_from_sap_cold_leveller",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_data_from_sap_cold_leveller_user_id",
                table: "plate_data_from_sap_cold_leveller",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_info_from_sap_module_id",
                table: "plate_info_from_sap",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_info_from_sap_user_id",
                table: "plate_info_from_sap",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_marking_data_for_receipe_module_id",
                table: "plate_marking_data_for_receipe",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_marking_record_module_id",
                table: "plate_marking_record",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_marking_record_plate_id",
                table: "plate_marking_record",
                column: "plate_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_punching_data_for_receipe_module_id",
                table: "plate_punching_data_for_receipe",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_punching_record_module_id",
                table: "plate_punching_record",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_plate_punching_record_plate_id",
                table: "plate_punching_record",
                column: "plate_id");

            migrationBuilder.CreateIndex(
                name: "IX_sap_credentials_module_id",
                table: "sap_credentials",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_tcp_config_module_id",
                table: "tcp_config",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_tcp_config_user_id",
                table: "tcp_config",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_module_id",
                table: "user_role",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_role_id",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_user_id",
                table: "user_role",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "check_box_table");

            migrationBuilder.DropTable(
                name: "down_coiler_records");

            migrationBuilder.DropTable(
                name: "plate_data_from_sap_cold_leveller");

            migrationBuilder.DropTable(
                name: "plate_marking_data_for_receipe");

            migrationBuilder.DropTable(
                name: "plate_marking_record");

            migrationBuilder.DropTable(
                name: "plate_punching_data_for_receipe");

            migrationBuilder.DropTable(
                name: "plate_punching_record");

            migrationBuilder.DropTable(
                name: "punching_cycle_status");

            migrationBuilder.DropTable(
                name: "sap_credentials");

            migrationBuilder.DropTable(
                name: "tcp_config");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "plate_info_from_sap");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSI_PPMS.Migrations
{
    public partial class newColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdDate",
                table: "user",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "idDeleted",
                table: "user",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdDate",
                table: "user");

            migrationBuilder.DropColumn(
                name: "idDeleted",
                table: "user");
        }
    }
}

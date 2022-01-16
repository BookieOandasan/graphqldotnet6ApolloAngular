using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQlApi.Migrations
{
    public partial class addMoreNoteField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsUrgent",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Notes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IsUrgent",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Notes");
        }
    }
}

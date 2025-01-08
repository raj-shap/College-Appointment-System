using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace College_Appointment_System.Migrations
{
    /// <inheritdoc />
    public partial class Updatestudentandprofessormodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Students",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Professors",
                newName: "UserName");

            migrationBuilder.AddColumn<Guid>(
                name: "Role",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Role",
                table: "Professors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Professors");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Students",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Professors",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

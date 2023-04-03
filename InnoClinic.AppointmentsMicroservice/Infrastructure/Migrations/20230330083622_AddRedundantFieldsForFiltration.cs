using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRedundantFieldsForFiltration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Results",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DoctorFirstName",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorLastName",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorMiddleName",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OfficeId",
                table: "Appointments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "DoctorFirstName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorLastName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorMiddleName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "Appointments");
        }
    }
}

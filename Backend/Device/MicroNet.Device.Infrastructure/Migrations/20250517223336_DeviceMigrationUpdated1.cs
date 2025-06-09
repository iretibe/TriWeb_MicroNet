using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Device.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeviceMigrationUpdated1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "device",
                table: "Devices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                schema: "device",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "device",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "device",
                table: "Devices");
        }
    }
}

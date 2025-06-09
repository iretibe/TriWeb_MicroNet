using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SystemUpdatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AuditInfo_CreatedAt",
                schema: "system",
                table: "CompanySetups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuditInfo_CreatedBy",
                schema: "system",
                table: "CompanySetups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AuditInfo_UpdatedAt",
                schema: "system",
                table: "CompanySetups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuditInfo_UpdatedBy",
                schema: "system",
                table: "CompanySetups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditInfo_CreatedAt",
                schema: "system",
                table: "CompanySetups");

            migrationBuilder.DropColumn(
                name: "AuditInfo_CreatedBy",
                schema: "system",
                table: "CompanySetups");

            migrationBuilder.DropColumn(
                name: "AuditInfo_UpdatedAt",
                schema: "system",
                table: "CompanySetups");

            migrationBuilder.DropColumn(
                name: "AuditInfo_UpdatedBy",
                schema: "system",
                table: "CompanySetups");
        }
    }
}

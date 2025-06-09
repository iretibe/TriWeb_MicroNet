using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Sundry.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SundryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sundry");

            migrationBuilder.CreateTable(
                name: "Accounting",
                schema: "sundry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    AuditInfo_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditInfo_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounting", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounting",
                schema: "sundry");
        }
    }
}

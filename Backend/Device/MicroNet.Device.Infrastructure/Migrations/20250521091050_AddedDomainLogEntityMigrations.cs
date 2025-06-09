using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Device.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDomainLogEntityMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_domainEventLogs",
                schema: "device",
                table: "domainEventLogs");

            migrationBuilder.RenameTable(
                name: "domainEventLogs",
                schema: "device",
                newName: "DomainEventLogs",
                newSchema: "device");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DomainEventLogs",
                schema: "device",
                table: "DomainEventLogs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DomainEventLogs",
                schema: "device",
                table: "DomainEventLogs");

            migrationBuilder.RenameTable(
                name: "DomainEventLogs",
                schema: "device",
                newName: "domainEventLogs",
                newSchema: "device");

            migrationBuilder.AddPrimaryKey(
                name: "PK_domainEventLogs",
                schema: "device",
                table: "domainEventLogs",
                column: "Id");
        }
    }
}

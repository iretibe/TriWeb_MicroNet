using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Pos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PosMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pos");

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "pos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentChannel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositorIdType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositorIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgentPin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationNetwork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditInfo_DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "pos");
        }
    }
}

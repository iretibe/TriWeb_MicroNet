using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Account.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AccountMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.CreateTable(
                name: "AccountTerminations",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminatedAccount_AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerminatedAccount_AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerminatedAccount_Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TerminatedAccount_BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditInfo_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditInfo_DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditInfo_ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTerminations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransfers",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceAccount_AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceAccount_AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceAccount_Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SourceAccount_BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationBranch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditInfo_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditInfo_DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditInfo_ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawals",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Network = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditInfo_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditInfo_DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditInfo_DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditInfo_ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawals", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTerminations",
                schema: "account");

            migrationBuilder.DropTable(
                name: "AccountTransfers",
                schema: "account");

            migrationBuilder.DropTable(
                name: "Withdrawals",
                schema: "account");
        }
    }
}

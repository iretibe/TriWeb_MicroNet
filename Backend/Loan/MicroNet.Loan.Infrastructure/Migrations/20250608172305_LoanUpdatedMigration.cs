using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Loan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LoanUpdatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanDocument_LoanApplications_LoanApplicationId",
                schema: "loan",
                table: "LoanDocument");

            migrationBuilder.DropTable(
                name: "LoanApplications",
                schema: "loan");

            migrationBuilder.RenameColumn(
                name: "LoanApplicationId",
                schema: "loan",
                table: "LoanDocument",
                newName: "LoanRequestId");

            migrationBuilder.CreateTable(
                name: "LoanRequests",
                schema: "loan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RepaymentPeriod = table.Column<int>(type: "int", nullable: false),
                    MaximumAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RequestedPrincipal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RiskMargin = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InsuranceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DisbursementMedium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReviewerComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditInfo_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditInfo_DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRequests", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDocument_LoanRequests_LoanRequestId",
                schema: "loan",
                table: "LoanDocument",
                column: "LoanRequestId",
                principalSchema: "loan",
                principalTable: "LoanRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanDocument_LoanRequests_LoanRequestId",
                schema: "loan",
                table: "LoanDocument");

            migrationBuilder.DropTable(
                name: "LoanRequests",
                schema: "loan");

            migrationBuilder.RenameColumn(
                name: "LoanRequestId",
                schema: "loan",
                table: "LoanDocument",
                newName: "LoanApplicationId");

            migrationBuilder.CreateTable(
                name: "LoanApplications",
                schema: "loan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisbursementMedium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LoanType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaximumAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RepaymentPeriod = table.Column<int>(type: "int", nullable: false),
                    RequestedPrincipal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ReviewerComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskMargin = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    AuditInfo_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditInfo_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditInfo_DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditInfo_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditInfo_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDocument_LoanApplications_LoanApplicationId",
                schema: "loan",
                table: "LoanDocument",
                column: "LoanApplicationId",
                principalSchema: "loan",
                principalTable: "LoanApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

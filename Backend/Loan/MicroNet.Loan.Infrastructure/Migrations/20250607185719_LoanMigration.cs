using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Loan.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LoanMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "loan");

            migrationBuilder.CreateTable(
                name: "LoanApplications",
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
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanDocument",
                schema: "loan",
                columns: table => new
                {
                    LoanApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDocument", x => new { x.LoanApplicationId, x.Id });
                    table.ForeignKey(
                        name: "FK_LoanDocument_LoanApplications_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalSchema: "loan",
                        principalTable: "LoanApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanDocument",
                schema: "loan");

            migrationBuilder.DropTable(
                name: "LoanApplications",
                schema: "loan");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Branch.Api.Migrations
{
    /// <inheritdoc />
    public partial class BranchMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "branch");

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SetupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchManagerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherProductSummaries",
                schema: "branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumberOfLoans = table.Column<int>(type: "int", nullable: false),
                    TotalLoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRepayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProcessingFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PenaltyCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLoanBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Withdrawal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ManagementFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherProductSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherProductSummaries_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "branch",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherProductSummaries_BranchId",
                schema: "branch",
                table: "OtherProductSummaries",
                column: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherProductSummaries",
                schema: "branch");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "branch");
        }
    }
}

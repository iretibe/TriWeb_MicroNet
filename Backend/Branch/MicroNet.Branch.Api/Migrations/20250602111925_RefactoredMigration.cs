using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Branch.Api.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherProductSummaries",
                schema: "branch");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "branch",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "branch",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "PhysicalAddress",
                schema: "branch",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "branch",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "BranchManagerName",
                schema: "branch",
                table: "Branches",
                newName: "Street");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchCode",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "AuditInfo_DeletedAt",
                schema: "branch",
                table: "Branches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuditInfo_DeletedBy",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "branch",
                table: "Branches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalAddress",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "branch",
                table: "Branches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                schema: "branch",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BranchTerminationRules",
                schema: "branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_BranchTerminationRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSummary",
                schema: "branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSummary_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "branch",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSummary_BranchId",
                schema: "branch",
                table: "ProductSummary",
                column: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchTerminationRules",
                schema: "branch");

            migrationBuilder.DropTable(
                name: "ProductSummary",
                schema: "branch");

            migrationBuilder.DropColumn(
                name: "AuditInfo_DeletedAt",
                schema: "branch",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "AuditInfo_DeletedBy",
                schema: "branch",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "branch",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                schema: "branch",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "PostalAddress",
                schema: "branch",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "branch",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "branch",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "Street",
                schema: "branch",
                table: "Branches",
                newName: "BranchManagerName");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BranchCode",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "branch",
                table: "Branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "branch",
                table: "Branches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalAddress",
                schema: "branch",
                table: "Branches",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "branch",
                table: "Branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "OtherProductSummaries",
                schema: "branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ManagementFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfLoans = table.Column<int>(type: "int", nullable: false),
                    PenaltyCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProcessingFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLoanBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRepayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Withdrawal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
    }
}

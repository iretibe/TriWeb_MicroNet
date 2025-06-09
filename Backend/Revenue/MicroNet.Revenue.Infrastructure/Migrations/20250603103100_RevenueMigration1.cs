using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Revenue.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RevenueMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "revenue");

            migrationBuilder.RenameTable(
                name: "Transactions",
                schema: "transaction",
                newName: "Transactions",
                newSchema: "revenue");

            migrationBuilder.RenameTable(
                name: "RevenueReversals",
                schema: "transaction",
                newName: "RevenueReversals",
                newSchema: "revenue");

            migrationBuilder.RenameTable(
                name: "PenaltyCharges",
                schema: "transaction",
                newName: "PenaltyCharges",
                newSchema: "revenue");

            migrationBuilder.RenameTable(
                name: "ManagementFees",
                schema: "transaction",
                newName: "ManagementFees",
                newSchema: "revenue");

            migrationBuilder.RenameTable(
                name: "InterestDistributions",
                schema: "transaction",
                newName: "InterestDistributions",
                newSchema: "revenue");

            migrationBuilder.RenameTable(
                name: "InterestDistributionAccounts",
                schema: "transaction",
                newName: "InterestDistributionAccounts",
                newSchema: "revenue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "transaction");

            migrationBuilder.RenameTable(
                name: "Transactions",
                schema: "revenue",
                newName: "Transactions",
                newSchema: "transaction");

            migrationBuilder.RenameTable(
                name: "RevenueReversals",
                schema: "revenue",
                newName: "RevenueReversals",
                newSchema: "transaction");

            migrationBuilder.RenameTable(
                name: "PenaltyCharges",
                schema: "revenue",
                newName: "PenaltyCharges",
                newSchema: "transaction");

            migrationBuilder.RenameTable(
                name: "ManagementFees",
                schema: "revenue",
                newName: "ManagementFees",
                newSchema: "transaction");

            migrationBuilder.RenameTable(
                name: "InterestDistributions",
                schema: "revenue",
                newName: "InterestDistributions",
                newSchema: "transaction");

            migrationBuilder.RenameTable(
                name: "InterestDistributionAccounts",
                schema: "revenue",
                newName: "InterestDistributionAccounts",
                newSchema: "transaction");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SystemMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "system");

            migrationBuilder.CreateTable(
                name: "CompanySetups",
                schema: "system",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OfficialEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OfficialPhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    YearOfRegistration = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LogoFileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LogoContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CoreBanking = table.Column<bool>(type: "bit", nullable: false),
                    TelcoIntegration = table.Column<bool>(type: "bit", nullable: false),
                    PaymentGateway = table.Column<bool>(type: "bit", nullable: false),
                    SftpPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ExportPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BatchImport = table.Column<bool>(type: "bit", nullable: false),
                    NotificationMode = table.Column<int>(type: "int", nullable: false),
                    NotificationRecipients = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UseMakerChecker = table.Column<bool>(type: "bit", nullable: false),
                    RequireLimit = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySetups", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanySetups",
                schema: "system");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroNet.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "employee");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialMediaAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSystemUser = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateEmployed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContactPerson_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson_GhanaCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson_PrimaryPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson_AlternatePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson_PrimaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson_AlternateEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson_PhysicalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson_PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson_SocialMedia = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "employee");
        }
    }
}

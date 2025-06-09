using MicroNet.System.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.System.Infrastructure.Configurations
{
    public class CompanySetupConfiguration : IEntityTypeConfiguration<CompanySetup>
    {
        public void Configure(EntityTypeBuilder<CompanySetup> builder)
        {
            builder.ToTable("CompanySetups");

            builder.HasKey(c => c.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.CompanyAddress).IsRequired().HasMaxLength(300);
            builder.Property(c => c.RegistrationDate).IsRequired();
            builder.Property(c => c.OfficialEmail).HasMaxLength(100);
            builder.Property(c => c.OfficialPhoneNumber).HasMaxLength(50);
            builder.Property(c => c.YearOfRegistration).HasMaxLength(10);
            builder.Property(c => c.SSN).HasMaxLength(50);
            builder.Property(c => c.TIN).HasMaxLength(50);
            builder.Property(c => c.Prefix).HasMaxLength(20);

            // Owned type: ContactPerson
            builder.OwnsOne(c => c.ContactPerson, cp =>
            {
                cp.Property(p => p.FirstName).HasColumnName("ContactFirstName").HasMaxLength(100);
                cp.Property(p => p.LastName).HasColumnName("ContactLastName").HasMaxLength(100);
                cp.Property(p => p.Email).HasColumnName("ContactEmail").HasMaxLength(100);
                cp.Property(p => p.PhoneNumber).HasColumnName("ContactPhone").HasMaxLength(50);
            });

            // Owned type: CompanyLogo
            builder.OwnsOne(c => c.Logo, logo =>
            {
                logo.Property(l => l.FileName).HasColumnName("LogoFileName").HasMaxLength(200);
                logo.Property(l => l.Content).HasColumnName("LogoContent");
            });

            // Owned type: IntegrationSettings
            builder.OwnsOne(c => c.IntegrationSettings, i =>
            {
                i.Property(p => p.CoreBankingEnabled).HasColumnName("CoreBanking");
                i.Property(p => p.TelcoIntegrationEnabled).HasColumnName("TelcoIntegration");
                i.Property(p => p.PaymentGatewayEnabled).HasColumnName("PaymentGateway");
                i.Property(p => p.SftpPath).HasColumnName("SftpPath").HasMaxLength(200);
                i.Property(p => p.ExportFolderPath).HasColumnName("ExportPath").HasMaxLength(200);
                i.Property(p => p.BatchTransactionImportEnabled).HasColumnName("BatchImport");
            });

            // Owned type: NotificationSettings
            builder.OwnsOne(c => c.NotificationSettings, n =>
            {
                n.Property(p => p.Mode).HasColumnName("NotificationMode");
                n.Property(p => p.UseMakerChecker).HasColumnName("UseMakerChecker");
                n.Property(p => p.RequireTransactionLimit).HasColumnName("RequireLimit");

                n.Property(p => p.Recipients)
                 .HasConversion(
                     v => string.Join(";", v),
                     v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                 )
                 .HasColumnName("NotificationRecipients")
                 .HasMaxLength(500);
            });

            builder.OwnsOne(x => x.AuditInfo, audit =>
            {
                audit.Property(a => a.CreatedBy).HasMaxLength(100);
                audit.Property(a => a.CreatedAt);
                audit.Property(a => a.UpdatedBy).HasMaxLength(100);
                audit.Property(a => a.UpdatedAt);
            });
        }
    }
}

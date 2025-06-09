namespace MicroNet.System.Core.Dtos
{
    public class IntegrationSettingsDto
    {
        public bool CoreBankingEnabled { get; set; }
        public bool TelcoIntegrationEnabled { get; set; }
        public bool PaymentGatewayEnabled { get; set; }
        public string SftpPath { get; set; } = default!;
        public string ExportFolderPath { get; set; } = default!;
        public bool BatchTransactionImportEnabled { get; set; }
    }
}

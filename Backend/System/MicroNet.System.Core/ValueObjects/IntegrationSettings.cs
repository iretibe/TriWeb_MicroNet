namespace MicroNet.System.Core.ValueObjects
{
    public class IntegrationSettings
    {
        public bool CoreBankingEnabled { get; private set; }
        public bool TelcoIntegrationEnabled { get; private set; }
        public bool PaymentGatewayEnabled { get; private set; }
        public string? SftpPath { get; private set; }
        public string? ExportFolderPath { get; private set; }
        public bool BatchTransactionImportEnabled { get; private set; }

        public IntegrationSettings(
            bool coreBankingEnabled,
            bool telcoIntegrationEnabled,
            bool paymentGatewayEnabled,
            string? sftpPath,
            string? exportFolderPath,
            bool batchTransactionImportEnabled)
        {
            CoreBankingEnabled = coreBankingEnabled;
            TelcoIntegrationEnabled = telcoIntegrationEnabled;
            PaymentGatewayEnabled = paymentGatewayEnabled;
            SftpPath = sftpPath;
            ExportFolderPath = exportFolderPath;
            BatchTransactionImportEnabled = batchTransactionImportEnabled;
        }
    }
}

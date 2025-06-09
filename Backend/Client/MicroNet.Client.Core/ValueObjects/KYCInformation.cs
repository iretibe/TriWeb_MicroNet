namespace MicroNet.Client.Core.ValueObjects
{
    public class KYCInformation
    {
        public string DocumentType { get; private set; }
        public string DocumentNumber { get; private set; }
        public DateTime ExpiryDate { get; private set; }

        private KYCInformation() { }

        public KYCInformation(string documentType, string documentNumber, DateTime expiryDate)
        {
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            ExpiryDate = expiryDate;
        }
    }
}

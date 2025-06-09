namespace MicroNet.Client.Core.Dtos
{
    public class KYCInformationDto
    {
        public string DocumentType { get; set; } = default!;
        public string DocumentNumber { get; set; } = default!;
        public DateTime ExpiryDate { get; set; }
    }
}

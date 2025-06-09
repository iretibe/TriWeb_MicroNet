namespace MicroNet.Revenue.Core.Dtos
{
    public class AccountShareDto
    {
        public string AccountNumber { get; set; } = default!;
        public decimal InterestAmount { get; set; }
    }
}

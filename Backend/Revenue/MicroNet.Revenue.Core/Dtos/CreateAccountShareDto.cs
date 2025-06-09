namespace MicroNet.Revenue.Core.Dtos
{
    public class CreateAccountShareDto
    {
        public string AccountNumber { get; set; } = default!;
        public decimal InterestAmount { get; set; }
    }
}

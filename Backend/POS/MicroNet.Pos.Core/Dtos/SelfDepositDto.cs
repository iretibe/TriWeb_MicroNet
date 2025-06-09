namespace MicroNet.Pos.Core.Dtos
{
    public class SelfDepositDto
    {
        public string AccountNumber { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reference { get; set; } = default!;
        public string Otp { get; set; } = default!;
        public string DepositorName { get; set; } = default!;
        public string DepositorIdType { get; set; } = default!;
        public string DepositorIdNumber { get; set; } = default!;
        public string AgenPin { get; set; } = default!;
    }
}

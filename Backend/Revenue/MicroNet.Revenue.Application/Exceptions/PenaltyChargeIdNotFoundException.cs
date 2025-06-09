namespace MicroNet.Revenue.Application.Exceptions
{
    public class PenaltyChargeIdNotFoundException : AppException
    {
        public PenaltyChargeIdNotFoundException(Guid code) : base($"Penalty Charge with Id: `{code}` is not found.")
        {
        }
    }
}

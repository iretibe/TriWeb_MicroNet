using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Pos.Application.Events
{
    public class TransactionCompletedEvent : EventBase
    {
        public Guid TransactionId { get; set; }
        public string AccountNumber { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reference { get; set; } = default!;
        public string DestinationType { get; set; } = default!;

        public TransactionCompletedEvent() { } //For deserialization

        public TransactionCompletedEvent(Guid transactionId, string accountNumber,
            decimal amount, string reference, string destinationType)
        {
            TransactionId = transactionId;
            AccountNumber = accountNumber;
            Amount = amount;
            Reference = reference;
            DestinationType = destinationType;
        }
    }
}

using MicroNet.Revenue.Core.ValueObjects;

namespace MicroNet.Revenue.Core.Entities
{
    public class Transaction : AggregateRoot
    {
        public ClientAccount Receiver { get; private set; }
        public decimal Amount { get; private set; }
        public string Reference { get; private set; }
        public DepositorId DepositorId { get; private set; }
        public string DepositorName { get; private set; }
        public string DestinationType { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public Transaction() { } //For EF Core


        private readonly List<Transaction> _transactions = new();
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        public Transaction(Guid id, ClientAccount receiver, decimal amount, string reference,
            DepositorId depositorId, string depositorName, string destinationType, string createdBy)
        {
            Id = id;
            Receiver = receiver;
            Amount = amount;
            Reference = reference;
            DepositorId = depositorId;
            DepositorName = depositorName;
            DestinationType = destinationType;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);

            //Raise domain events
            //DomainEvents.Raise(new TransactionAddedEvent(transaction));
        }
    }
}

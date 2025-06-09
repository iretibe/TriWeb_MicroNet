using MicroNet.Product.Core.ValueObjects;

namespace MicroNet.Product.Core.Entities
{
    public class Product : AggregateRoot
    {
        public string ProductCode { get; private set; }
        public string ProductName { get; private set; }
        public string Description { get; private set; }
        public string? Note { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        private Product() { }

        public Product(string code, string name, string description, string? note, string createdBy)
        {
            ProductCode = code;
            ProductName = name;
            Description = description;
            Note = note;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }

        public void Update(Guid id, string name, string description, string? note, string updatedBy)
        {
            Id = id;
            ProductName = name;
            Description = description;
            Note = note;
            AuditInfo.SetUpdated(updatedBy);
        }
    }
}

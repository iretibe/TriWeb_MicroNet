using MediatR;
using MicroNet.Device.Core.ValueObjects;

namespace MicroNet.Device.Core.Entities
{
    public class Device : AggregateRoot
    {
        public DeviceCode Code { get; private set; } = default!;
        public DeviceName Name { get; private set; } = default!;
        public DeviceDescription Description { get; private set; } = default!;
        public DeviceNotes Notes { get; private set; } = default!;
        public AuditInfo AuditInfo { get; private set; } = default!;
        public bool IsDeleted { get; private set; } = false;

        private readonly List<INotification> _domainEvents = new();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        //EF Core
        public Device() { }

        public Device(DeviceCode code, DeviceName name, 
            DeviceDescription description, DeviceNotes notes, AuditInfo auditInfo)
        {
            Id = new AggregateId(Guid.NewGuid());
            Code = code;
            Name = name;
            Description = description;
            Notes = notes;
            AuditInfo = auditInfo;
            IsDeleted = false;
        }

        protected void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public static Device AddDevice(DeviceCode code, DeviceName name, 
            DeviceDescription description, DeviceNotes notes, AuditInfo auditInfo)
        {
            return new Device(code, name, description, notes, auditInfo);
        }

        public void UpdateDevice(Guid id, DeviceCode code, DeviceName name, 
            DeviceDescription description, DeviceNotes notes, string createdBy, string updatedBy)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Notes = notes;
            AuditInfo = new AuditInfo(
                AuditInfo.CreatedBy,                
                AuditInfo.CreatedAt,
                updatedBy,
                DateTime.UtcNow,
                AuditInfo.DeletedBy!,
                AuditInfo.DeletedAt
            );
        }

        public void DeleteDevice(Guid id, string deletedBy)
        {
            if (IsDeleted) return;

            IsDeleted = true;
            AuditInfo = new AuditInfo(
                AuditInfo.CreatedBy,
                AuditInfo.CreatedAt,
                AuditInfo.UpdatedBy!,
                AuditInfo.UpdatedAt,
                deletedBy,
                DateTime.UtcNow
            );
        }
    }
}

using MicroNet.Shared.CQRS.Events;

namespace MicroNet.User.Application.Events.User
{
    public class UserUpdatedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime OccurredAt { get; private set; }
        public string TriggeredBy { get; private set; }

        public string UserId { get; private set; }
        public string FullName { get; private set; }
        public string PhysicalAddress { get; private set; }
        public string PostalAddressId { get; private set; }
        public string UserImage { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string CreateBy { get; private set; }
        public bool Status { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public UserUpdatedEvent(string triggeredBy, string userId, string fullName, string physicalAddress,
            string postalAddressId, string userImage, DateTime createDate, string createBy, bool status,
            string userName, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
            OccurredAt = DateTime.Now;
            TriggeredBy = triggeredBy;
            UserId = userId;
            FullName = fullName;
            PhysicalAddress = physicalAddress;
            PostalAddressId = postalAddressId;
            UserImage = userImage;
            CreateDate = createDate;
            CreateBy = createBy;
            Status = status;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}

namespace MicroNet.Device.Application.Dto
{
    public class AllDeviceDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Notes { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string CreatedByName { get; set; } = default!;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public string? DeletedByName { get; set; }
        public bool IsDeleted { get; set; }
    }
}

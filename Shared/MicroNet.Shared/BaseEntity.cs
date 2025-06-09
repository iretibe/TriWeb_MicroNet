using System.ComponentModel.DataAnnotations;

namespace MicroNet.Shared
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = default!;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}

using Microsoft.AspNetCore.Identity;

namespace MicroNet.User.Core.Helper
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = default!;
        public string PhysicalAddress { get; set; } = default!;
        public string PostalAddress { get; set; } = default!;
        public string UserImage { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        public bool Status { get; set; }
        public string Code { get; set; } = default!;
        public bool IsSystemAdmin { get; set; }
        public bool IsFirstTimeLogin { get; set; }
    }
}

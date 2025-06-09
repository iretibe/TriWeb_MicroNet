// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;

namespace MicroNet.Identity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = default!;
        public string PhysicalAddress { get; set; } = default!;
        public string PostalAddress { get; set; } = default!;
        public string UserImage { get; set; } = default!;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public Guid CreateBy { get; set; }
        public bool Status { get; set; }
        public string Code { get; set; } = default!;
        public bool IsSystemAdmin { get; set; }
        public bool IsFirstTimeLogin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Core.Models;

[Index("NormalizedEmail", Name = "EmailIndex")]
public partial class AspNetUsers
{
    [Key]
    public string Id { get; set; } = default!;

    [Required]
    public string FullName { get; set; } = default!;

    [Required]
    public string PhysicalAddress { get; set; } = default!;

    [Required]
    public string PostalAddress { get; set; } = default!;

    [Required]
    public string UserImage { get; set; } = default!;

    public DateTime CreateDate { get; set; }

    public Guid CreateBy { get; set; }

    public bool Status { get; set; }

    [Required]
    public string Code { get; set; } = default!;

    public bool IsSystemAdmin { get; set; }

    public bool IsFirstTimeLogin { get; set; }

    [StringLength(256)]
    public string UserName { get; set; } = default!;

    [StringLength(256)]
    public string NormalizedUserName { get; set; } = default!;

    [StringLength(256)]
    public string Email { get; set; } = default!;

    [StringLength(256)]
    public string NormalizedEmail { get; set; } = default!;

    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; } = default!;

    public string SecurityStamp { get; set; } = default!;

    public string ConcurrencyStamp { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; } = new List<AspNetUserClaims>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; } = new List<AspNetUserLogins>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; } = new List<AspNetUserRoles>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; } = new List<AspNetUserTokens>();
}

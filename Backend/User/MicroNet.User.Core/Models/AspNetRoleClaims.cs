using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Core.Models;

[Index("RoleId", Name = "IX_AspNetRoleClaims_RoleId")]
public partial class AspNetRoleClaims
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string RoleId { get; set; } = default!;

    public string ClaimType { get; set; } = default!;

    public string ClaimValue { get; set; } = default!;

    [ForeignKey("RoleId")]
    [InverseProperty("AspNetRoleClaims")]
    public virtual AspNetRoles Role { get; set; } = default!;
}

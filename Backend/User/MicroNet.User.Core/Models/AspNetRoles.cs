using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Core.Models;

public partial class AspNetRoles
{
    [Key]
    public string Id { get; set; } = default!;

    [StringLength(256)]
    public string Name { get; set; } = default!;

    [StringLength(256)]
    public string NormalizedName { get; set; } = default!;

    public string ConcurrencyStamp { get; set; } = default!;

    [InverseProperty("Role")]
    public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaims>();

    [InverseProperty("Role")]
    public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; } = new List<AspNetUserRoles>();
}

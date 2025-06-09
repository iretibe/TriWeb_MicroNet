using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Core.Models;

[Index("RoleId", Name = "IX_AspNetUserRoles_RoleId")]
public partial class AspNetUserRoles
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(450)]
    public string UserId { get; set; } = default!;

    [Required]
    public string RoleId { get; set; } = default!;

    [ForeignKey("RoleId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual AspNetRoles Role { get; set; } = default!;

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual AspNetUsers User { get; set; } = default!;
}

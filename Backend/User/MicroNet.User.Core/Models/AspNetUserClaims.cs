using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Core.Models;

[Index("UserId", Name = "IX_AspNetUserClaims_UserId")]
public partial class AspNetUserClaims
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = default!;

    public string ClaimType { get; set; } = default!;

    public string ClaimValue { get; set; } = default!;

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserClaims")]
    public virtual AspNetUsers User { get; set; } = default!;
}

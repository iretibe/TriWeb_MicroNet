using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Core.Models;

[PrimaryKey("LoginProvider", "ProviderKey")]
[Index("UserId", Name = "IX_AspNetUserLogins_UserId")]
public partial class AspNetUserLogins
{
    [Key]
    public string LoginProvider { get; set; } = default!;

    [Key]
    public string ProviderKey { get; set; } = default!;

    public string ProviderDisplayName { get; set; } = default!;

    [Required]
    public string UserId { get; set; } = default!;

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserLogins")]
    public virtual AspNetUsers User { get; set; } = default!;
}

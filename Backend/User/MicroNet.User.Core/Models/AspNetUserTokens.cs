using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Core.Models;

[PrimaryKey("UserId", "LoginProvider", "Name")]
public partial class AspNetUserTokens
{
    [Key]
    public string UserId { get; set; } = default!;

    [Key]
    public string LoginProvider { get; set; } = default!;

    [Key]
    public string Name { get; set; } = default!;

    public string Value { get; set; } = default!;

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserTokens")]
    public virtual AspNetUsers User { get; set; } = default!;
}

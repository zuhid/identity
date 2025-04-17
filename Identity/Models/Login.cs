using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zuhid.Identity.Models;

public class Login
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(20, MinimumLength = 7)]
    public string Password { get; set; } = string.Empty;

    public string Tfa { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}

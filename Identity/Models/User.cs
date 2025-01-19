using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Zuhid.Base;

namespace Zuhid.Identity.Models;

public class User : BaseModel
{
    public bool IsActive { get; set; }
    [StringLength(50)]
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string Phone { get; set; } = "";
}

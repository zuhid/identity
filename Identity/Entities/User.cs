using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zuhid.Base;

namespace Zuhid.Identity.Entities;

public class User : BaseEntity
{
    public bool IsActive { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Phone { get; set; }
}


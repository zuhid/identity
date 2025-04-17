using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zuhid.Identity.Models;

namespace Zuhid.Identity.Controllers;

[Route("[controller]")]
public class LoginController : Controller
{
    [AllowAnonymous]
    [HttpPost()]
    public LoginResponse Login([FromBody] Login model)
    {
        return new LoginResponse
        {
            Token = $"{model?.UserName}:{model?.Password}"
        };
    }
}

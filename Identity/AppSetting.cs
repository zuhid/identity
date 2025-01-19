using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zuhid.Identity;

public class AppSetting(IConfiguration configuration)
{
    public string Name { get; set; } = "Identity";
    public string Version { get; set; } = "1.0";
    public string CorsOrigin { get; set; } = "CorsOrigin";
    public string IdentityContext { get; set; } = configuration.GetConnectionString("IdentityContext") ?? "";
    // public string IdentityContext { get; set; } = (configuration.GetConnectionString("IdentityContext") ?? "")
    //       .Replace("[sql_password]", configuration.GetValue<string>("sql_password")); // Replace "[sql_password]" with value from secrets
}


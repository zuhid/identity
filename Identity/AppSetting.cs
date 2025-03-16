namespace Zuhid.Identity;

public class AppSetting(IConfiguration configuration)
{
    public string Name { get; set; } = "Identity";
    public string Version { get; set; } = "1.0";
    public string CorsOrigin { get; set; } = "CorsOrigin";
    public string Identity { get; set; } = configuration.GetConnectionString("Identity") ?? "";
    public string Log { get; set; } = configuration.GetConnectionString("Log") ?? "";
    // public string Identity { get; set; } = (configuration.GetConnectionString("Identity") ?? "")
    //       .Replace("[sql_password]", configuration.GetValue<string>("sql_password")); // Replace "[sql_password]" with value from secrets
}


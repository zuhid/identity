namespace Zuhid.Identity;

internal class AppSetting(IConfiguration configuration)
{
    public string Name { get; set; } = "Identity";
    public string Version { get; set; } = "1.0";
    public string CorsOrigin { get; set; } = "CorsOrigin";
    public string Identity { get; set; } = GetConnectionString(configuration, "Identity");
    public string Log { get; set; } = GetConnectionString(configuration, "Log");
    private static string GetConnectionString(IConfiguration configuration, string connString)
    {
        return (configuration.GetConnectionString(connString) ?? "")
          .Replace("[db_credential]", configuration.GetValue<string>("db_credential"), StringComparison.Ordinal); // Replace "[db_credential]" with value from secrets
    }
}

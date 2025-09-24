namespace Zuhid.BaseApi;

public abstract class BaseAppSetting(IConfiguration configuration) {
  public abstract string Name { get; set; }
  public string Version { get; set; } = string.Empty;
  public string CorsOrigin { get; set; } = string.Empty;
  public IdentitySetting Identity { get; set; } = new();
  public string LogContext { get; set; } = GetConnectionString(configuration, "Log");
  protected static string GetConnectionString(IConfiguration configuration, string connString) {
    // Replace "[postgres_server]" with value from secrets
    return (configuration.GetConnectionString(connString) ?? "")
      .Replace("[postgres_server]", configuration.GetValue<string>("postgres_server"), StringComparison.Ordinal);
  }

  public class IdentitySetting {
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string SymmetricKey { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public double ExpireMinutes { get; set; } = 0;
  }
}

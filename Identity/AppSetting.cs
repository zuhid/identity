using Zuhid.BaseApi;

namespace Zuhid.Identity;

public class AppSetting : BaseAppSetting {
  public AppSetting(IConfiguration configuration) : base(configuration) {
    if (configuration != null) {
      configuration.GetSection("AppSetting").Bind(this);
      IdentityContext = GetConnectionString(configuration, "Identity");
    }
  }

  public override string Name { get; set; } = string.Empty;
  public string IdentityContext { get; set; } = string.Empty;
}

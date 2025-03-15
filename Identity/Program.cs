using Zuhid.Base;
using Zuhid.Identity;
using Zuhid.Identity.Mappers;

namespace Zuhid.Identity;

public class Program
{
  public static void Main(string[] args)
  {
    var app = new BaseWebApplication(args, "Zuhid.Identity", "1.0", "CorsOrigins");
    var appSetting = new AppSetting(app.builder.Configuration);
    app.AddServices();
    app.AddDatabase<IdentityContext, IdentityContext>(appSetting.IdentityContext);
    app.builder.Services.AddTransient<IIdentityRepository, IdentityRepository>();
    app.builder.Services.AddTransient<IUserMapper, UserMapper>();
    app.Build().Run();
  }
}

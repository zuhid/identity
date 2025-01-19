using Zuhid.Base;
using Zuhid.Identity;
using Zuhid.Identity.Mappers;

namespace Zuhid.Identity;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = new BaseWebApplication(args, "Zuhid.Identity", "1.0", "CorsOrigins");
        var appSetting = new AppSetting(builder.builder.Configuration);
        builder.AddServices();
        builder.AddDatabase<IdentityContext, IdentityContext>(appSetting.IdentityContext);
        builder.builder.Services.AddTransient<IdentityRepository, IdentityRepository>();
        builder.builder.Services.AddTransient<UserMapper, UserMapper>();



        builder.Build()
            .Run();
    }
}

using Zuhid.BaseApi;
using Zuhid.Identity.Mappers;

namespace Zuhid.Identity;

public class Program
{
    public static void Main(string[] args)
    {
        var app = new BaseWebApplication(args, "Zuhid.Identity", "1.0", "CorsOrigins");
        var appSetting = new AppSetting(app.Builder.Configuration);
        app.AddServices();
        app.AddDatabase<IdentityContext, IdentityContext>(appSetting.Identity);
        app.AddDatabase<LogContext, LogContext>(appSetting.Log);

        using (var databaseLoggerProvider = new DatabaseLoggerProvider(app.Builder.Services.BuildServiceProvider().GetRequiredService<LogContext>()))
        {
            app.Builder.Logging.AddProvider(databaseLoggerProvider);
        }
        app.Builder.Services.AddTransient<IIdentityRepository, IdentityRepository>();
        app.Builder.Services.AddTransient<IUserMapper, UserMapper>();
        app.Build().Run();
    }
}

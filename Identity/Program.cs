using Zuhid.Base;
using Zuhid.Identity.Mappers;

namespace Zuhid.Identity;

public class Program
{
    public static void Main(string[] args)
    {
        var app = new BaseWebApplication(args, "Zuhid.Identity", "1.0", "CorsOrigins");
        var appSetting = new AppSetting(app.builder.Configuration);
        app.AddServices();
        app.AddDatabase<IdentityContext, IdentityContext>(appSetting.Identity);
        app.AddDatabase<LogContext, LogContext>(appSetting.Log);

        using (var databaseLoggerProvider = new DatabaseLoggerProvider(app.builder.Services.BuildServiceProvider().GetRequiredService<LogContext>()))
        {
            app.builder.Logging.AddProvider(databaseLoggerProvider);
        }
        app.builder.Services.AddTransient<IIdentityRepository, IdentityRepository>();
        app.builder.Services.AddTransient<IUserMapper, UserMapper>();
        app.Build().Run();
    }
}

using Microsoft.AspNetCore.Identity;
using Zuhid.BaseApi;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Mappers;
using Zuhid.Identity.Repositories;

namespace Zuhid.Identity;

public class Program
{
    public static void Main(string[] args)
    {
        var app = new BaseWebApplication(args, "Zuhid.Identity", "1.0", "CorsOrigins", new IdentityModel
        {
            Audience = "https://localhost:4200",
            Issuer = "https://localhost:5215",
            SymmetricKey = "EQszUJwDn48B3dDFQxx7NyGMR5XL4mxSBbtx2D3B8jbRHxwnYzsunQqPCJvzKhnZYw4GMWYaGYfAWgtN2upSePG6F6TAdY9pYTy9y43WfxxJaShASJDTT5FWYXWAnnEx"
        });
        var appSetting = new AppSetting(app.Builder.Configuration);
        app.AddServices();
        app.AddDatabase<IdentityContext, IdentityContext>(appSetting.Identity);
        app.AddDatabase<LogContext, LogContext>(appSetting.Log);

        using (var databaseLoggerProvider = new DatabaseLoggerProvider(app.Builder.Services.BuildServiceProvider().GetRequiredService<LogContext>()))
        {
            app.Builder.Logging.AddProvider(databaseLoggerProvider);
        }
        app.Builder.Services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        // app.Builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
        // {
        //     options.TokenLifespan = TimeSpan.FromHours(1); // How long the email confirmation token is valid. Default is 24 hours, if not specified
        // });


        app.Builder.Services.AddTransient<IIdentityRepository, IdentityRepository>();
        app.Builder.Services.AddTransient<UserRepository, UserRepository>();
        app.Builder.Services.AddTransient<IUserMapper, UserMapper>();
        app.Build().Run();

        // var builtApp = app.Build();
        // var loggerProvider = builtApp.Services.GetRequiredService<DatabaseLoggerProvider>();
        // app.Builder.Logging.AddProvider(loggerProvider);
        // builtApp.Run();
    }
}

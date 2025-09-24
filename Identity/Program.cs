using Microsoft.AspNetCore.Identity;
using Zuhid.BaseApi;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Mappers;
using Zuhid.Identity.Repositories;

namespace Zuhid.Identity;

public static class Program {
  public static void Main(string[] args) {
    var builder = WebApplication.CreateBuilder(args);
    var appSetting = new AppSetting(builder.Configuration);
    builder.AddServices(appSetting);
    builder.AddDatabase<IdentityContext, IdentityContext>(appSetting.IdentityContext);

    builder.Services
        .AddIdentity<User, Role>()
        .AddEntityFrameworkStores<IdentityContext>()
        .AddDefaultTokenProviders();

    builder.Services
        .AddTransient<IIdentityRepository, IdentityRepository>()
        .AddTransient<UserRepository, UserRepository>()
        .AddTransient<IUserMapper, UserMapper>();
    builder.Build(appSetting).Run();
  }
}

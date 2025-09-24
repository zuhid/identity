using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zuhid.BaseApi;

public static class WebApplicationBuilderExtenstion {
  public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder, BaseAppSetting baseAppSetting) {
    var services = builder.Services;
    services.AddCors(options => options.AddPolicy(baseAppSetting.CorsOrigin, policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    ));
    services
        .AddAuthentication()
        .AddJwtBearer("Bearer", option => new JwtTokenService(baseAppSetting.Identity).Configure(option));
    services.AddControllers().AddMvcOptions(options => {
      // options.Filters.Add(new AuthorizeFilter());
      // options.Filters.Add(typeof(LogExceptionFilter));
      options.Filters.Add<ActionFilter>();
      options.Filters.Add<ExceptionFilter>();
    });
    services.AddSwaggerGen(options => AddSwagger(options, baseAppSetting.Name, baseAppSetting.Version));

    services.AddScoped<ITokenService>(option => new JwtTokenService(baseAppSetting.Identity)); // Add identity service
    services.AddScoped<IMessageService, MessageService>(); // Add message service

    if (!string.IsNullOrEmpty(baseAppSetting.LogContext)) {
      builder.AddDatabase<LogContext, LogContext>(baseAppSetting.LogContext);
      using var databaseLoggerProvider = new DatabaseLoggerProvider(services.BuildServiceProvider().GetRequiredService<LogContext>());
      builder.Logging.AddProvider(databaseLoggerProvider);
    }
    return builder;
  }

  public static void AddDatabase<ITContext, TContext>(this WebApplicationBuilder builder, string connectionString)
      where ITContext : class
      where TContext : DbContext, ITContext {
    builder.Services.AddDbContext<TContext>(options => options
      .UseNpgsql(connectionString)
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // setting to no tracking to improve performance
    );
    builder.Services.AddScoped<ITContext, TContext>();
  }

  public static WebApplication Build(this WebApplicationBuilder builder, BaseAppSetting baseAppSetting) {
    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{baseAppSetting.Version}/swagger.json", $"{baseAppSetting.Name} v{baseAppSetting.Version}"));
    app.UseCors(baseAppSetting.CorsOrigin);
    app.UseRouting();
    // app.UseAuthorization();

    // on the base page, show a simple message
    app.MapGet("/", async context => await context.Response.WriteAsync("<html><body style='padding:100px 0;text-align:center;font-size:xxx-large;'><a href='/swagger'>View Swagger</a></body></html>"));
    app.MapControllers();
    return app;
  }

  private static void AddSwagger(SwaggerGenOptions options, string title, string version) {
    options.SwaggerDoc($"v{version}", new OpenApiInfo {
      Title = $"{title} Api",
      Version = version
    });

    // add xml document
    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"Zuhid.{title}.xml");
    if (File.Exists(xmlPath)) {
      options.IncludeXmlComments(xmlPath);
    }

    // AddSecurityDefinition and AddSecurityRequirement needed to allow users to pass in jwt token
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
      In = ParameterLocation.Header,
      Description = "Please insert JWT with Bearer into field",
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {{
        new OpenApiSecurityScheme{
          Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          }
        },
        Array.Empty<string>()
        }});
    options.EnableAnnotations();
  }
}

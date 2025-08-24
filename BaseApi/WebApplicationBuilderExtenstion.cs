using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zuhid.BaseApi;

public class BaseWebApplication(string[] args, string title, string version, string corsOrigin)
{
    public WebApplicationBuilder Builder { get; private set; } = WebApplication.CreateBuilder(args);

    public BaseWebApplication AddServices()
    {
        var services = Builder.Services;
        services.AddControllers().AddMvcOptions(options =>
        {
            // options.Filters.Add(new AuthorizeFilter());
            // options.Filters.Add(typeof(LogExceptionFilter));
            options.Filters.Add<ActionFilter>();
            options.Filters.Add<ExceptionFilter>();
        });
        services.AddSwaggerGen(AddSwagger);
        return this;
    }

    public WebApplication Build()
    {
        var app = Builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{version}/swagger.json", $"{title} v{version}"));
        app.UseCors(corsOrigin);
        app.UseRouting();
        // app.UseAuthorization();

        // on the base page, show a simple message
        app.MapGet("/", async context => await context.Response.WriteAsync("<html><body style='padding:100px 0;text-align:center;font-size:xxx-large;'><a href='/swagger'>View Swagger</a></body></html>"));
        app.MapControllers();

        return app;
    }

    public void AddDatabase<ITContext, TContext>(string connectionString)
        where ITContext : class
        where TContext : DbContext, ITContext
    {
        Builder.Services.AddDbContext<TContext>(options => options
          .UseNpgsql(connectionString)
          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // setting to no tracking to improve performance
        );
        Builder.Services.AddScoped<ITContext, TContext>();
    }

    private void AddSwagger(SwaggerGenOptions options)
    {
        options.SwaggerDoc($"v{version}", new OpenApiInfo
        {
            Title = $"{title} Api",
            Version = version
        });

        // add xml document
        var xmlPath = Path.Combine(AppContext.BaseDirectory, $"Zuhid.{title}.xml");
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }

        // AddSecurityDefinition and AddSecurityRequirement needed to allow users to pass in jwt token
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
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

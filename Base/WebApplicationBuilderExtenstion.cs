using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.EntityFrameworkCore;

namespace Zuhid.Base;
public class BaseWebApplication
{
  private readonly string title;
  private readonly string version;
  private readonly string corsOrigin;
  public WebApplicationBuilder builder { get; private set; }

  public BaseWebApplication(string[] args, string title, string version, string corsOrigin)
  {
    this.corsOrigin = corsOrigin;
    this.version = version;
    this.title = title;
    this.builder = WebApplication.CreateBuilder(args);
  }

  public BaseWebApplication AddServices()
  {
    var services = builder.Services;
    services.AddControllers();
    services.AddSwaggerGen(options => AddSwagger(options));
    return this;
  }

  public WebApplication Build()
  {
    var app = builder.Build();
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
    builder.Services.AddDbContext<TContext>(options => options
      .UseSqlServer(connectionString)
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // setting to no tracking to improve performance
    );
    builder.Services.AddScoped<ITContext, TContext>();
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

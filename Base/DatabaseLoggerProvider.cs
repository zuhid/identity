using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Zuhid.Base;
public class DatabaseLoggerProvider(LogContext logContext) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new DatabaseLogger(logContext);

    public void Dispose() => GC.SuppressFinalize(this);
}

public class DatabaseLogger(LogContext logContext) : ILogger
{
    IDisposable ILogger.BeginScope<TState>(TState state) => null;
    public bool IsEnabled(LogLevel logLevel) => true;
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        logContext.Add(new Log
        {
            Updated = DateTime.UtcNow,
            LogLevel = logLevel.ToString(),
            Category = eventId.Name ?? "categoryName",
            EventId = $"{eventId.Id}",
            Exception = Formatter(state, exception)
        });
        try
        {
            logContext.SaveChanges();
        }
        catch (Exception ex)
        {
            // Ignore exception thrown during loggging
            Console.WriteLine(ex.Message);
        }
    }


    private string Formatter<TState>(TState state, Exception exception)
    {
        if (exception != null)
        {
            var stacktrace = new List<object>();
            var stepList = exception.StackTrace?.Split(" at ") ?? [];
            for (int i = 0; i < stepList.Length; i++)
            {
                string item = stepList[i].Trim();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var index = item.IndexOf(" in ");
                    if (index > 0)
                    {
                        stacktrace.Add(new { At = item.Substring(0, index).Trim(), In = item.Substring(index + 3).Trim() });
                    }
                    else
                    {
                        stacktrace.Add(new { At = item });
                    }
                }
            }
            return JsonSerializer.Serialize(
              new { exception.Message, stacktrace, exception.Data },
              new JsonSerializerOptions
              {
                  PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                  DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                  Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
              }
            );
        }
        else
        {
            return state?.ToString() ?? "";
        }
    }
}


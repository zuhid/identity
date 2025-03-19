using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zuhid.Base;
public class DatabaseLoggerProvider(LogContext logContext) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new DatabaseLogger(logContext, categoryName);

    public void Dispose() => GC.SuppressFinalize(this);
}

public class DatabaseLogger(LogContext logContext, string categoryName) : ILogger
{
    IDisposable ILogger.BeginScope<TState>(TState state) => null;
    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        logContext.Add(new Log
        {
            Updated = DateTime.UtcNow,
            LogLevel = logLevel.ToString(),
            Category = categoryName,
            EventId = $"{eventId.Id}",
            EventName = eventId.Name ?? "",
            State = state?.ToString() ?? "",
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


    private static string Formatter<TState>(TState state, Exception exception)
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
                        stacktrace.Add(new { At = item[..index].Trim(), In = item[(index + 3)..].Trim() });
                    }
                    else
                    {
                        stacktrace.Add(new { At = item });
                    }
                }
            }
            return JsonSerializer.Serialize(new { exception.Message, stacktrace, exception.Data }, jsonSerializerOptions);
        }
        else
        {
            return string.Empty;
        }
    }

    private static JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}


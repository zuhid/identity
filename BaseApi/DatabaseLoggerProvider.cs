using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zuhid.BaseApi;

public class DatabaseLoggerProvider(LogContext logContext) : ILoggerProvider {
  public ILogger CreateLogger(string categoryName) => new DatabaseLogger(logContext, categoryName);

  public void Dispose() => GC.SuppressFinalize(this);
}

public class DatabaseLogger(LogContext logContext, string categoryName) : ILogger {
  public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
  public bool IsEnabled(LogLevel logLevel) => true;

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
    logContext.Add(new Log {
      Updated = DateTime.UtcNow,
      LogLevel = logLevel.ToString(),
      Category = categoryName,
      EventId = $"{eventId.Id}",
      EventName = eventId.Name ?? "",
      State = state?.ToString() ?? "",
      Exception = Formatter(exception)
    });
    try {
      logContext.SaveChanges();
    } catch (Exception ex) {
      // Ignore exception thrown during loggging
      Console.WriteLine(ex.Message);
    }

    // var queryString = $@"insert into public.Log(id,updated,log_level,category,event_id,event_name,state,exception) values(@Id,@Updated,@LogLevel,@Category,@EventId,@EventName,@State,@Exception)";
    // using var connection = new NpgsqlConnection(logContext.Database.GetConnectionString());
    // using var command = new NpgsqlCommand(queryString, connection);
    // command.Parameters.AddWithValue("@Id", Guid.NewGuid());
    // command.Parameters.AddWithValue("@Updated", DateTime.UtcNow);
    // command.Parameters.AddWithValue("@LogLevel", logLevel.ToString());
    // command.Parameters.AddWithValue("@Category", categoryName ?? "none");
    // command.Parameters.AddWithValue("@EventId", $"{eventId.Id}");
    // command.Parameters.AddWithValue("@EventName", eventId.Name ?? "");
    // command.Parameters.AddWithValue("@State", state?.ToString() ?? "");
    // command.Parameters.AddWithValue("@Exception", Formatter(exception));

    // try {
    //   command.Connection?.Open();
    //   command.ExecuteNonQuery();
    // } catch (Exception ex) {
    //   Console.WriteLine(ex.Message); // Ignore exception thrown during loggging
    // } finally {
    //   command.Connection?.Close();
    // }
  }

  private static string Formatter(Exception? exception) {
    if (exception != null) {
      var stacktrace = new List<object>();
      var stepList = exception.StackTrace?.Split(" at ") ?? [];
      for (var i = 0; i < stepList.Length; i++) {
        var item = stepList[i].Trim();
        if (!string.IsNullOrWhiteSpace(item)) {
          var index = item.IndexOf(" in ");
          if (index > 0) {
            stacktrace.Add(new { At = item[..index].Trim(), In = item[(index + 3)..].Trim() });
          } else {
            stacktrace.Add(new { At = item });
          }
        }
      }
      return JsonSerializer.Serialize(new { exception.Message, stacktrace, exception.Data }, JsonSerializerOption);
    } else {
      return string.Empty;
    }
  }

  private static readonly JsonSerializerOptions JsonSerializerOption = new() {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };
}

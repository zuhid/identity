namespace Zuhid.BaseApi;

public class Log
{
    public Guid Id { get; set; }
    public required DateTime Updated { get; set; }
    public string LogLevel { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public string EventName { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Exception { get; set; } = string.Empty;
}

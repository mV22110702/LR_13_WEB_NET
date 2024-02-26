namespace LR_13_WEB_NET;

public class WinLogModel
{
    public Int64 TimeCreated { get; set; }
    public LogLevel Level { get; set; }
    public string Message { get; set; } = string.Empty;
    public Int64 EventId { get; set; }
    public string Computer { get; set; } = Environment.MachineName;
}
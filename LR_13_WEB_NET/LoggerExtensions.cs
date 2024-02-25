using OpenTelemetry;
using OpenTelemetry.Logs;

namespace LR_13_WEB_NET;

public static class LoggerExtensions
{
    public static OpenTelemetryLoggerOptions AddFileExporter(this OpenTelemetryLoggerOptions options, string path)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        return options.AddProcessor(new BatchLogRecordExportProcessor(new FileExporter(path)));
    }
}
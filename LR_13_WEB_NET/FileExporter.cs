using System.Text.Json.Serialization;
using Newtonsoft.Json;
using OpenTelemetry;
using OpenTelemetry.Logs;

namespace LR_13_WEB_NET;

class FileExporter : BaseExporter<LogRecord>
{
    private readonly string _path;

    public FileExporter(string path)
    {
        _path = path;
    }

    public override ExportResult Export(in Batch<LogRecord> batch)
    {
        foreach (var record in batch)
        {
            File.AppendAllText(
                _path,
                JsonConvert.SerializeObject(record) + Environment.NewLine
            );
        }

        return ExportResult.Success;
    }
}
using LR_13_WEB_NET;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


var builder = WebApplication.CreateBuilder(args);

var resource = ResourceBuilder.CreateDefault()
    .AddService(TelemetryConstants.ServiceName, TelemetryConstants.ServiceVersion);

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(resource)
        .AddConsoleExporter()
        .AddFileExporter(".\\file-log-exporter.json");
});
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
        resource.AddService(TelemetryConstants.ServiceName, TelemetryConstants.ServiceVersion))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter(o => { o.Endpoint = new Uri("http://localhost:4317"); })
    )
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
        .AddOtlpExporter(o => { o.Endpoint = new Uri("http://localhost:4317"); })
        .AddConsoleExporter()
    );

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
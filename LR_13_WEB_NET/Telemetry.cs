using System.Diagnostics;

namespace LR_13_WEB_NET;

public static class TelemetryConstants
{
    public static readonly string ServiceName = "WeatherForecastService";
    public static readonly string ServiceVersion = "1.0.0";
}

public static class Telemetry
{
    public static readonly ActivitySource WeatherActivitySource = new(TelemetryConstants.ServiceName);
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LR_13_WEB_NET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("for-next-days/{numOfDays:int}")]
        public IEnumerable<WeatherForecast> Get(int numOfDays)
        {
            using var myActivity = Telemetry.WeatherActivitySource.StartActivity(name:
                $"Getting forecast for the next {numOfDays} days");
            myActivity.SetTag("NumOfDays", numOfDays);
            return Enumerable.Range(1, numOfDays).Select(index =>
                {
                    using var activity = Telemetry.WeatherActivitySource.StartActivity(name:
                        $"Getting forecast for day {index}");
                    activity.SetTag("Day", index);
                    var forecast = new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    };
                    var tags = new ActivityTagsCollection()
                    {
                        { "DateTime", forecast.Date }, { "Temp", forecast.TemperatureC },
                        { "Summary", forecast.Summary }
                    };
                    activity.AddEvent(new("Forecast generated", DateTimeOffset.Now, tags));
                    return forecast;
                })
                .ToArray();
        }
    }
}
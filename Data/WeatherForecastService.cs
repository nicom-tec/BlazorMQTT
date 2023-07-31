using System.Globalization;

namespace BlazorMQTT.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public async Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
        {
            WeatherForecast [] wfArray  =  Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray()).Result;

            #pragma warning disable CS4014 // Da auf diesen Aufruf nicht gewartet wird, wird die Ausführung der aktuellen Methode vor Abschluss des Aufrufs fortgesetzt.
            Task.Run(() =>
            {
                foreach (WeatherForecast weatherForecast in wfArray)
                {
                    MQTTClient.Publish((weatherForecast.Date.ToString("d", new CultureInfo("de-DE"))) + "/temperature", weatherForecast.TemperatureC.ToString());
                    MQTTClient.Publish((weatherForecast.Date.ToString("d", new CultureInfo("de-DE"))) + "/summary", weatherForecast.Summary);
                }
            });
            #pragma warning restore CS4014 // Da auf diesen Aufruf nicht gewartet wird, wird die Ausführung der aktuellen Methode vor Abschluss des Aufrufs fortgesetzt.

            return wfArray;
        }
    }
}
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

            foreach( WeatherForecast weatherForecast in wfArray )
            {
                await MQTTClient.Publish((weatherForecast.Date.ToShortDateString()) + "/temperature", weatherForecast.TemperatureC.ToString());
                await MQTTClient.Publish((weatherForecast.Date.ToShortDateString()) + "/summary", weatherForecast.Summary);
            }


            return wfArray;
        }
    }
}
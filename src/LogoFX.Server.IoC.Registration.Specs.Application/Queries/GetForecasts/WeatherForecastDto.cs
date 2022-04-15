using System;

namespace LogoFX.Server.IoC.Registration.Specs.Domain
{
    public class WeatherForecastDto
    {
        public WeatherForecastDto(WeatherForecast entity)
        {
            Date = entity.Date;
            TemperatureC = entity.TemperatureC;
            TemperatureF = entity.TemperatureF;
            Summary = entity.Summary;
        }
        public DateTime Date { get; private set; }

        public int TemperatureC { get; private set; }

        public int TemperatureF { get; private set; }

        public string Summary { get; private set; }
    }
}
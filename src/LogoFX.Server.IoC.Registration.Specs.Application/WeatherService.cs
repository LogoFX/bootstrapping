using System;
using System.Collections.Generic;
using System.Linq;
using LogoFX.Server.IoC.Registration.Specs.Domain;

namespace LogoFX.Server.IoC.Registration.Specs.Application
{
    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        IEnumerable<WeatherForecast> IWeatherService.GetForecasts()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}
using System.Collections.Generic;

namespace LogoFX.Server.IoC.Registration.Specs.Domain
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetForecasts();
    }
}

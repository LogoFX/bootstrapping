using System.Collections.Generic;

namespace LogoFX.Server.IoC.Registration.Specs.Domain
{
    public interface IWeatherRepository
    {
        IEnumerable<WeatherForecast> GetForecasts();
    }
}

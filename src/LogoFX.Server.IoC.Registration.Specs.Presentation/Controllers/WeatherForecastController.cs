using LogoFX.Server.IoC.Registration.Specs.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LogoFX.Server.IoC.Registration.Specs.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherService.GetForecasts();
    }
}
using LogoFX.Server.IoC.Registration.Specs.Application.Queries.GetForecasts;
using LogoFX.Server.IoC.Registration.Specs.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LogoFX.Server.IoC.Registration.Specs.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecastDto>> Get(
        [FromServices] GetForecastsInteractor getForecastsInteractor)
    {
        return await getForecastsInteractor.ExecuteAsync();
    }
}
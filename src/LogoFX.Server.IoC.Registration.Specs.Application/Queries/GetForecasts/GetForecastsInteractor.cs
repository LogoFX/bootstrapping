using System.Linq;
using System.Threading.Tasks;
using LogoFX.Server.IoC.Abstractions.Interactors;
using LogoFX.Server.IoC.Registration.Specs.Domain;

namespace LogoFX.Server.IoC.Registration.Specs.Application.Queries.GetForecasts
{
    public sealed class GetForecastsInteractor : IInteractor
    {
        private readonly IWeatherRepository _weatherRepository;

        public GetForecastsInteractor(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<WeatherForecastDto[]> ExecuteAsync()
        {
            var forecasts = await Task.Run(() => _weatherRepository.GetForecasts());
            return forecasts
                .Select(entity => new WeatherForecastDto(entity))
                .ToArray();
        }
    }
}

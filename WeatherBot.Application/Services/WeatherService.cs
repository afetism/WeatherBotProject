using WeatherBot.Application.DTOs;
using WeatherBot.Application.Interfaces.Services;

namespace WeatherBot.Application.Services;

public class WeatherService : IWeatherService
{

    public Task<WeatherDto> GetWeatherAsync(string city)
    {
        throw new NotImplementedException();
    }

    public Task SaveWeatherHistory(long telegramUserId, WeatherDto weather)
    {
        throw new NotImplementedException();
    }
}

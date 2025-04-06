using WeatherBot.API.DTOs;

namespace WeatherBot.API.Services
{
    public interface IWeatherService
    {
        Task<string> ProcessUserWeatherRequestAsync(WeatherRequestDto weatherRequest);
        Task SendWeatherToAllUsersAsync(string city);
    }
}

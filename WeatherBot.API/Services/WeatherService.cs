using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net.Http;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Requests.Abstractions;
using WeatherBot.API.DTOs;
using WeatherBot.API.Entities;
using WeatherBot.API.Repositories;

namespace WeatherBot.API.Services;

public class WeatherService : IWeatherService
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration configuration;
    private readonly ITelegramBotClient telegramBotClient;
    private readonly HttpClient httpClient;
   

    public WeatherService(IUserRepository userRepository, IConfiguration configuration, ITelegramBotClient telegramBotClient, HttpClient httpClient)
    {
        this.userRepository = userRepository;
        this.configuration = configuration;
        this.telegramBotClient = telegramBotClient;
        this.httpClient = httpClient;
    }

    public async Task<string> ProcessUserWeatherRequestAsync(WeatherRequestDto weatherRequest)
    {
        var user = await userRepository.GetByTelegramIdAsync(weatherRequest.TelegramId);
        if (user is null)
        {
            user = new User { TelegramId = weatherRequest.TelegramId, Username = "Unknown" };
            await userRepository.AddUserAsync(user);
        }
        var weatherInfo = await GetWeatherAsync(weatherRequest.City);
        var message = $"Şəhər: {weatherRequest.City}\n{weatherInfo}";

        await userRepository.AddWeatherHistoryAsync(new WeatherHistory
        {
            UserId = user.Id,
            City = weatherRequest.City,
            WeatherInfo = weatherInfo
        });
        await telegramBotClient.SendTextMessageAsync(
            chatId: weatherRequest.TelegramId,
            text: message
        );

        return weatherInfo;
    }

    public async Task SendWeatherToAllUsersAsync(string city)
    {
        var weatherInfo = await GetWeatherAsync(city);
        var message = $"Şəhər: {city}\n{weatherInfo}";
        var users = await userRepository.GetAllUsersAsync();

        foreach (var user in users)
        {
            try
            {
                await telegramBotClient.SendTextMessageAsync(chatId: user.TelegramId, text: message);
            }
            catch
            {
               
            }
        }
    }

    public async Task<string> GetWeatherAsync(string city)
    {
        var apiKey = configuration["OpenWeatherMap:ApiKey"];
        var response = await httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=az");

        if (!response.IsSuccessStatusCode)
            return "Hava məlumati alina bilmedi. ";

        var json = await response.Content.ReadAsStringAsync();
        using var doc= JsonDocument.Parse(json);
        var main = doc.RootElement.GetProperty("main");
        var temp = main.GetProperty("temp").GetDecimal();
        var feelsLike = main.GetProperty("feels_like").GetDecimal();
        var description = doc.RootElement
            .GetProperty("weather")[0]
            .GetProperty("description")
            .GetString();

        return $"Hava: {description}\nTemperatur: {temp}°C\nHissedilən: {feelsLike}°C";
    }


}


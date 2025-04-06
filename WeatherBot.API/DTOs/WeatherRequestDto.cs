namespace WeatherBot.API.DTOs;

public class WeatherRequestDto
{
    public long TelegramId { get; set; }
    public string City { get; set; } = null!;
}

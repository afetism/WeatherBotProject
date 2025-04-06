namespace WeatherBot.API.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    public long TelegramId { get; set; }
    public string? Username { get; set; }
    public List<WeatherHistory>? WeatherHistory { get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

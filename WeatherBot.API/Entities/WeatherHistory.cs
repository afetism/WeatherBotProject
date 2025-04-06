namespace WeatherBot.API.Entities;

public class WeatherHistory : IEntity
{
    public int Id { get ; set; }
    public int UserId { get;set; }

    public string City {  get; set; }
    public string WeatherInfo { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.Now;
}

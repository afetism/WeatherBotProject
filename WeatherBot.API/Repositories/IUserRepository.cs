using WeatherBot.API.Entities;

namespace WeatherBot.API.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetUserWithHistoryAsync(int id);
    Task<User?> GetByTelegramIdAsync(long telegramId);
    Task AddWeatherHistoryAsync(WeatherHistory weatherHistory);
    Task<IEnumerable<User>> GetAllUsersAsync();



}

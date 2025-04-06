using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

using WeatherBot.API.Entities;

namespace WeatherBot.API.Repositories;

public class UserRepository : IUserRepository
{

    private readonly IDbConnection _db;

    public UserRepository(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
    public async Task AddUserAsync(User user)
    {
        var sql = "INSERT INTO Users(TelegramId,UserName,CreatedAt) VALUES (@TelegramId,@Username,@CreatedAt)";
        await _db.ExecuteAsync(sql, user);
    }

    public async Task AddWeatherHistoryAsync(WeatherHistory weatherHistory)
    {
       var sql = "INSERT INTO WeatherHistory(UserId,City,WeatherInfo,RequestedAt) VALUES (@UserId,@City,@WeatherInfo,@RequestedAt)";
        await _db.ExecuteAsync(sql, weatherHistory);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var sql = "SELECT * FROM Users";
        return await _db.QueryAsync<User>(sql);
    }

    public async Task<User?> GetByTelegramIdAsync(long telegramId)
    {
        var sql = "SELECT* FROM Users WHERE TelegramId=@TelegramId";
        return await _db.QueryFirstOrDefaultAsync<User>(sql,new { TelegramId = telegramId });
    }

    public async Task<User?> GetUserWithHistoryAsync(int id)
    {
        var sqlUser = "SELECT * FROM Users WHERE Id=@Id";
        var user= await _db.QueryFirstOrDefaultAsync(sqlUser,new { Id = id });
        if (user is not null)
        {
            var sqlHistory = "SELECT * FROM WeatherHistory WHERE UserId=@Id,ORDER BY RequestedAt DESC";
            var history = await _db.QueryAsync<WeatherHistory>(sqlHistory,new {Id=id});
            user.WeatherHistory = history.ToList();
        }
        return user;
    }
}

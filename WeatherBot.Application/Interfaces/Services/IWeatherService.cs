using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBot.Application.DTOs;

namespace WeatherBot.Application.Interfaces.Services;

public interface IWeatherService
{
    Task<WeatherDto> GetWeatherAsync(string city);
    Task SaveWeatherHistory(long telegramUserId, WeatherDto weather);

}

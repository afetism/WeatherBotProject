using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherBot.Application.DTOs;

namespace WeatherBot.Infrastructure.ExternalApis;

public class OpenWeatherMapClient
{
     private readonly HttpClient _httpClient;
     private readonly string _apiKey;

    public OpenWeatherMapClient(IConfiguration configuration)
    {
        _httpClient = new();
        _apiKey = configuration["OpenWeatherMap:ApiKey"];
    }


    public async Task<WeatherDto?> GetWeatherAsync(string city)
    {
        var url= $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=az";
        var response = await _httpClient.GetAsync(url) ;
        
        if(!response.IsSuccessStatusCode)
            return null ;

        var content = await response.Content.ReadAsStringAsync();

        var json = JsonDocument.Parse(content) ;



        return new WeatherDto
        {
            City = json.RootElement.GetProperty("name").GetString() ?? city,
            Temperature = json.RootElement.GetProperty("main").GetProperty("temp").GetSingle(),
            Description = json.RootElement.GetProperty("weather")[0].GetProperty("description").GetString() ?? ""
        };

    }

}

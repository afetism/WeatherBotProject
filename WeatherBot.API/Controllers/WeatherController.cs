using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherBot.API.DTOs;
using WeatherBot.API.Services;

namespace WeatherBot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestWeather([FromBody] WeatherRequestDto request)
        {
            var result = await _weatherService.ProcessUserWeatherRequestAsync(request);
            return Ok(result);
        }

        [HttpPost("sendToAll")]
        public async Task<IActionResult> SendWeatherToAll([FromBody] string city)
        {
            await _weatherService.SendWeatherToAllUsersAsync(city);
            return Ok("Göndərildi.");
        }
    }
}

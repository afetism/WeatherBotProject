using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBot.Application.DTOs;

public class WeatherDto
{
    public string City { get; set; } = string.Empty;
    public float Temperature { get; set; }
    public string Description { get; set; }=string.Empty;

}

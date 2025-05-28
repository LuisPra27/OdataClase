using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;


public class WeatherForecastsController : ODataController
{
    private static List<WeatherForecast> _data = new()
    {
        new WeatherForecast { Id = 1, Date = DateTime.Now, TemperatureC = 25, Summary = "Sunny" },
        new WeatherForecast { Id = 2, Date = DateTime.Now.AddDays(1), TemperatureC = 18, Summary = "Cloudy" },
        new WeatherForecast { Id = 3, Date = DateTime.Now.AddDays(2), TemperatureC = 30, Summary = "Hot" },
        new WeatherForecast { Id = 4, Date = DateTime.Now.AddDays(3), TemperatureC = 15, Summary = "Cool" }
    };

    [EnableQuery]
    public IQueryable<WeatherForecast> Get()
    {
        return _data.AsQueryable();
    }

    [HttpPost]
    public IActionResult Post([FromBody] WeatherForecast forecast)
    {
        if (forecast == null)
            return BadRequest();

        // Asigna un nuevo Id incremental
        forecast.Id = _data.Count > 0 ? _data.Max(f => f.Id) + 1 : 1;
        _data.Add(forecast);
        return Created(forecast);
    }
}

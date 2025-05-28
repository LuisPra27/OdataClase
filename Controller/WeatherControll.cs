using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;


public class WeatherForecastsController : ODataController
{
    private static List<WeatherForecast> _data = new()
    {
        new WeatherForecast { Id = 1, Date = DateTime.Now, TemperatureC = 25, Summary = "Soleado" },
        new WeatherForecast { Id = 2, Date = DateTime.Now.AddDays(1), TemperatureC = 18, Summary = "Nublado" },
        new WeatherForecast { Id = 3, Date = DateTime.Now.AddDays(2), TemperatureC = 30, Summary = "Caliente" },
        new WeatherForecast { Id = 4, Date = DateTime.Now.AddDays(3), TemperatureC = 15, Summary = "Frio" },
        new WeatherForecast { Id = 5, Date = DateTime.Now.AddDays(4), TemperatureC = 20, Summary = "Templado" },
        new WeatherForecast { Id = 6, Date = DateTime.Now.AddDays(5), TemperatureC = 22, Summary = "Lluvioso" }
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

        forecast.Id = _data.Count > 0 ? _data.Max(f => f.Id) + 1 : 1;
        _data.Add(forecast);
        return Created(forecast);
    }
}

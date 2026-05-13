
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebAPIPrime.Data;
using WebAPIPrime.Models;

namespace WebAPIPrime.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(AppDbContext db) : ControllerBase
    {
        private static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList();

            db.WeatherForecasts.AddRangeAsync(data);
            db.SaveChangesAsync();
            return data;
        }

        [HttpGet("{id}")]
        public WeatherForecast Get(int id)
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            var result = list.Sum();
            return new WeatherForecast() { TemperatureC = result };
        }

        // POST api/weatherforecast
        [HttpPost]
        public ActionResult<WeatherForecast> Create([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
                return BadRequest("Forecast cannot be null");

            forecast.Date = DateOnly.FromDateTime(DateTime.Now);
            db.WeatherForecasts.AddRangeAsync(forecast);
            db.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = 1 }, forecast);
        }
    }
}

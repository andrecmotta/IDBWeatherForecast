using IDBWeatherForecastBackend.Models;
using IDBWeatherForecastBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDBWeatherForecastBackend.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastServices _weatherForecastServices;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastServices weatherForecastService)
        {
            _logger = logger;
            _weatherForecastServices = weatherForecastService; 

        }
        [ResponseCache(Duration = 300)]
        [Route("location"),HttpGet]
        public ActionResult<Location> GetWeatherLocation(string latitude, string longitude)
        {
            var location =_weatherForecastServices.GetLocation(latitude, longitude);
            if (location == null)
                return NotFound();
            return Ok(location);

        }
        [ResponseCache(Duration = 300)]
        [Route("{locationkey}"), HttpGet]
        public ActionResult<WeatherCondition> GetCurrentWeather(string locationKey)
        {
            var currentWeather = _weatherForecastServices.GetCurrentWeatherCondition(locationKey);
            if (currentWeather == null)
                return NotFound();
            return Ok(currentWeather);

        }
        [ResponseCache(Duration = 300)]
        [Route("forecast/{locationkey}"), HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecast(string locationKey, bool isMetric=true)
        {
            var weatherForecast = _weatherForecastServices.GetWeatherForecast(locationKey,isMetric);
            if (!weatherForecast.Any())
                return NotFound();
            return Ok(weatherForecast);

        }



    }
}
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
        /// <summary>
        /// Gets the location from the latitude and longitude.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>Location</returns>
        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "latitude", "longitude" })]
        [Route("location"), HttpGet]
        public ActionResult<Location> GetWeatherLocation(string latitude, string longitude)
        {
            var location = _weatherForecastServices.GetLocation(latitude, longitude);
            if (location == null)
                return NotFound();
            return Ok(location);

        }
        /// <summary>
        /// Get the current weather for that location
        /// </summary>
        /// <param name="locationKey">This key is provided by the GetWeatherLocation method</param>
        /// <returns>WeatherCondition</returns>
        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "locationKey" })]
        [Route("{locationkey}"), HttpGet]
        public ActionResult<WeatherCondition> GetCurrentWeather(string locationKey)
        {
            var currentWeather = _weatherForecastServices.GetCurrentWeatherCondition(locationKey);
            if (currentWeather == null)
                return NotFound();
            return Ok(currentWeather);

        }
        /// <summary>
        /// Get the 5 days weather forecast. 
        /// </summary>
        /// <param name="locationKey">This key is provided by the GetWeatherLocation method</param>
        /// <param name="isMetric">This method returns only one system, set this parameter to false to return Imperial system.</param>
        /// <returns>List of WeatherForecast</returns>
        [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "locationKey", "isMetric" })]
        [Route("forecast/{locationkey}"), HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> GetWeatherForecast(string locationKey, bool isMetric = true)
        {
            var weatherForecast = _weatherForecastServices.GetWeatherForecast(locationKey, isMetric);
            if (!weatherForecast.Any())
                return NotFound();
            return Ok(weatherForecast);

        }
    }
}
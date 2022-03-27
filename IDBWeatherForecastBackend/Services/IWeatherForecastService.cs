using IDBWeatherForecastBackend.Models;

namespace IDBWeatherForecastBackend.Services
{
    public interface IWeatherForecastServices
    {
        public Location? GetLocation(string latitude, string longitude);
        public WeatherCondition? GetCurrentWeatherCondition(string locationKey);
        public IEnumerable<WeatherForecast> GetWeatherForecast(string locationKey, bool isMetric);
    }
}

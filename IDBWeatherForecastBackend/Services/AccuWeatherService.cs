using IDBWeatherForecastBackend.Models;
using IDBWeatherForecastBackend.Settings;
using System.Text.Json;

namespace IDBWeatherForecastBackend.Services
{
    public class AccuWeatherService : IWeatherForecastServices
    {
        private IHttpClientFactory _httpClientFactory;
        private ILogger _logger;
        private AccuweatherOptions _accuweatherSettings;
        public AccuWeatherService(IHttpClientFactory httpClientFactory, ILogger<AccuWeatherService> logger, AccuweatherOptions accuweatherSettings)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _accuweatherSettings = accuweatherSettings;
        }

        //Gets the current weather information
        public WeatherCondition? GetCurrentWeatherCondition(string locationKey)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_accuweatherSettings.WeatherCurrentConditionsApiURL);
            string urlParameters = $"{locationKey}?apikey={_accuweatherSettings.apiKey}&details=true";
            try
            {
                var response = client.GetAsync(urlParameters).Result;
                JsonSerializerOptions serializerOptions = new();
                serializerOptions.IncludeFields = true;
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<IEnumerable<WeatherCondition>>(serializerOptions).Result;
                    if (result != null) return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        //Gets the location based on latitude and longitude
        public Location? GetLocation(string latitude, string longitude)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_accuweatherSettings.LocationApiUrl);
            string urlParameters = $"?q={latitude},{longitude}&apikey={_accuweatherSettings.apiKey}";
            try
            {
                var response = client.GetAsync(urlParameters).Result;
                JsonSerializerOptions serializerOptions = new();
                serializerOptions.IncludeFields = true;
                if (response != null && response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<Location>(serializerOptions).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        //Gets the 5 day forecast
        //Contrary to the GetCurrentWeather method, this method does not respond with information for both metric and imperial
        //One call for each unit system is necessary
        public IEnumerable<WeatherForecast> GetWeatherForecast(string locationKey, bool isMetric)
        {

            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_accuweatherSettings.WeatherForecastApiURL);
            string urlParameters = $"{locationKey}?apikey={_accuweatherSettings.apiKey}&details=true&metric={isMetric}";
            try
            {
                var response = client.GetAsync(urlParameters).Result;
                JsonSerializerOptions serializerOptions = new();
                serializerOptions.IncludeFields = true;
                if (response != null && response.IsSuccessStatusCode)
                {


                    var result = response.Content.ReadFromJsonAsync<DailyForecast>(serializerOptions).Result;
                    if (result?.DailyForecasts != null && result.DailyForecasts.Any()) return result.DailyForecasts;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return new List<WeatherForecast>();
        }
    }
    //This class is only used to deserialize the response from accuweather
    public class DailyForecast
    {
        public IEnumerable<WeatherForecast>? DailyForecasts { get; set; }
    }
}

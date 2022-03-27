namespace IDBWeatherForecastBackend.Settings
{
    public class AccuweatherOptions
    {
        public const string Accuweather = "Accuweather";
        public string LocationApiUrl { get; set; } = string.Empty;
        public string WeatherCurrentConditionsApiURL { get; set; } = string.Empty;
        public string WeatherForecastApiURL { get; set; } = string.Empty;
        public string apiKey { get; set; } = string.Empty;
    }
}

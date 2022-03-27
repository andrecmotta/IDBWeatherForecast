namespace IDBWeatherForecastBackend.Models
{
    public class WeatherForecast
    {
        //Date of the forecast, displayed as the number of seconds that have elapsed since January 1, 1970 (midnight UTC/GMT).
        public long EpochDate { get; set; }
        public Information? Day { get; set; }
        public Information? Night { get; set; }
        public MinimuMaximum? Temperature { get; set; }
    }
    public class MinimuMaximum
    {
        public Measure? Maximum { get; set; }
        public Measure? Minimum { get; set; }
    }
    public class Information
    {
        //Percent representing the probability of precipitation. May be NULL.
        public int? PrecipitationProbability { get; set; }
        //Numeric value representing an icon that matches the forecast.
        public int Icon { get; set; }
        //	Phrase description of the icon.
        public string IconPhrase { get; set; } = string.Empty;
        //Rounded speed value of the Wind and Type of unit of the wind.
        public ForecastWind? Wind { get; set; }
    }

    public class ForecastWind
    {
        public Speed? Speed;
    }
    public class Speed
    {
        public float? Value { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}

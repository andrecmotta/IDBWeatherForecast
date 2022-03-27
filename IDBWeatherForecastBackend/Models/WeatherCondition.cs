namespace IDBWeatherForecastBackend.Models
{
    public class WeatherCondition
    {
        //DateTime of the current observation, displayed as the number of seconds that have elapsed since January 1, 1970 (midnight UTC/GMT).
        public long EpochTime { get; set; }
        //Phrase description of the current weather condition.Displayed in the language set with language code in URL.
        public String WeatherText { get; set; } = string.Empty;
        //Value and Unit of temperature in Both Metric (C) and Imperial (F) units.
        public UnitSystems? Temperature { get; set; }
        //Numeric value representing an image that displays the current condition described by WeatherText.May be NULL.
        public int? WeatherIcon { get; set; }
        //Relative humidity. May be NULL.
        public int? RelativeHumidity { get; set; }
        //Wind speed and direction
        public Wind? Wind { get; set; }
        //Flag indicating the time of day(true=day, false=night)
        public bool IsDayTime { get; set; }

    }

    public class Measure
    {
        public decimal? Value { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
    public class Wind
    {
        public Direction? Direction { get; set; }
        public UnitSystems? Speed { get; set; }
    }

    //Imperial and Metric Unit Systems
    public class UnitSystems
    {
        public Measure? Metric { get; set; }
        public Measure? Imperial { get; set; }
    }
    public class Direction
    {
        public string English { get; set; } = string.Empty;
        public int? Degrees { get; set; }
    }
}

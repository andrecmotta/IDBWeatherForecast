namespace IDBWeatherForecastBackend.Models
{

    public class Location
    {
        //Location key. Used to inform the Weather API the location.
        public string? Key { get; set; }
        //Location name displayed in English.
        public string? EnglishName { get; set; }
        //Country name displayed in English.
        public InformationWithEnglishName Country { get; set; } = new();
        //Administrative Area name displayed in English.
        public InformationWithEnglishName AdministrativeArea { get; set; } = new ();
        //Location name displayed in English.
        public InformationWithEnglishName ParentCity { get; set; } = new ();
        //Number of hours offset from local GMT time.
        public TimeZone TimeZone { get; set; } = new();
    }


    public class InformationWithEnglishName
    {
        public string? EnglishName { get; set; }
    }
    public class TimeZone
    {
        public decimal GmtOffset { get; set; }
    }

}

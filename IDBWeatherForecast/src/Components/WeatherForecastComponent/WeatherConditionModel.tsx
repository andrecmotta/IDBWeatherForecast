interface WeatherConditionModel {
    epochTime: number;
    weatherText: string;
    temperatureCelcius: number;
    temperatureFarenheits: number;
    weatherIcon: string;
    relativeHumidity: string;
    windMetric: string;
    windImperial: string;
    isDayTime: boolean;
}

export default WeatherConditionModel;
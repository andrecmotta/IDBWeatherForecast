interface WeatherForecastModel {
    epochTime: number;
    preciptationProbability: number;
    weatherIcon: string;
    maxTemperature: number;
    minTemperature: number;
    weatherText: string;
}

export default WeatherForecastModel;
import React from 'react';
import { render, screen } from '@testing-library/react';
import WeatherForecast from './WeatherForecastComponent';

var newWeatherForecast = () => {
    return new WeatherForecast({}, { isLoading: false, currentUnit: 'metro', fiveDayForecast: [], location: undefined, weatherCondition: undefined, isOnError: false });
}
test('Adding Location', () => {
    var wfc: WeatherForecast = newWeatherForecast();
    wfc.state = { currentUnit: "metric", fiveDayForecast: [], location: { city: "Arlington", country: "United States", state: "Virginia", key: 12345, parentCity: undefined }, weatherCondition: undefined, isLoading: false, isOnError: false };
    var element = wfc.renderLocation();
    render(element);
    const linkElement = screen.getByText(/Arlington, Virginia/i);
    expect(linkElement).toBeInTheDocument();
    screen.debug();
});
test('Loading', () => {
    var wfc: WeatherForecast = newWeatherForecast();
    wfc.state = { currentUnit: "metric", fiveDayForecast: [], location: { city: "Arlington", country: "United States", state: "Virginia", key: 12345, parentCity: undefined }, weatherCondition: undefined, isLoading: true, isOnError: false };
    render(wfc.render());
    const linkElement = screen.getByRole("loading");
    expect(linkElement).toBeInTheDocument();
    screen.debug();
})
test('Error', () => {
    var wfc: WeatherForecast = newWeatherForecast();
    wfc.state = { currentUnit: "metric", fiveDayForecast: [], location: { city: "Arlington", country: "United States", state: "Virginia", key: 12345, parentCity: undefined }, weatherCondition: undefined, isLoading: false, isOnError: true };
    render(wfc.render());
    const linkElement = screen.getByRole("error");
    expect(linkElement).toBeInTheDocument();
    screen.debug();
})
test('Metric', () => {
    var wfc: WeatherForecast = newWeatherForecast();
    wfc.state = {
        currentUnit: "metric", fiveDayForecast: [], location: { city: "Arlington", country: "United States", state: "Virginia", key: 12345, parentCity: undefined }, weatherCondition: { epochTime: 12345, isDayTime: true, relativeHumidity: '25%', temperatureCelcius: 10, temperatureFarenheits: 50, weatherIcon: '6', weatherText: 'Sunny', windImperial: '10mi/h', windMetric: '16km/h' }, isLoading: false, isOnError: false
    };
    var element = wfc.renderCurrentWeather();
    render(element);
    const linkElement = screen.getByText("10");
    expect(linkElement).toBeInTheDocument();
    screen.debug();
})
test('Imperial', () => {
    var wfc: WeatherForecast = newWeatherForecast();
    wfc.state = {
        currentUnit: "imperial", fiveDayForecast: [], location: { city: "Arlington", country: "United States", state: "Virginia", key: 12345, parentCity: undefined }, weatherCondition: { epochTime: 12345, isDayTime: true, relativeHumidity: '25%', temperatureCelcius: 10, temperatureFarenheits: 50, weatherIcon: '6', weatherText: 'Sunny', windImperial: '10mi/h', windMetric: '16km/h' }, isLoading: false, isOnError: false
    };
    var element = wfc.renderCurrentWeather();
    render(element);
    const linkElement = screen.getByText("50");
    expect(linkElement).toBeInTheDocument();
    screen.debug();
})

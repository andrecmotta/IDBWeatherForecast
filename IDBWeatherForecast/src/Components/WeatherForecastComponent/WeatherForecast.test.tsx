import React from 'react';
import { render, screen } from '@testing-library/react';
import WeatherForecast from './WeatherForecastComponent';

test('Location', () => {
    var wfc: WeatherForecast = new WeatherForecast({}, { currentUnit: "metric", fiveDayForecast: [], location: { city: "Arlingon", country: "United States", state: "Virginia", key: 12345, parentCity: "" }, weatherCondition: undefined, isLoading:false });
    wfc.setState({})
    var element = wfc.renderLocation();
    render(element);
    const linkElement = screen.getByText("Arlington"); 
  expect(linkElement).toBeInTheDocument();
});

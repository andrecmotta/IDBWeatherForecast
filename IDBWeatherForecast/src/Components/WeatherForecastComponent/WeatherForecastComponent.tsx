import React from 'react';
import LocationModel from './LocationModel';
import WeatherConditionModel from './WeatherConditionModel';
import './WeatherForecast.css'
import * as moment from 'moment';
import Cookies, { CookieSetOptions } from 'universal-cookie';
import WeatherForecastModel from './WeatherForecastModel';


interface WeatherForecastProps {

}

interface WeatherForecastState {
    location?: LocationModel;
    weatherCondition?: WeatherConditionModel;
    currentUnit: string;
    fiveDayForecast: Array<WeatherForecastModel>;
    isLoading: boolean;
    isOnError: boolean;
}

class WeatherForecast extends React.Component<WeatherForecastProps, WeatherForecastState> {
    cookies = new Cookies();
    fiveDayForecastMetric: Array<WeatherForecastModel> = [];
    fiveDayForecastImperial: Array<WeatherForecastModel> = [];
    public constructor(props: WeatherForecastProps, state: WeatherForecastState) {
        super(props, state);

        var currentUnit = this.cookies.get("currentUnit");
        if (!currentUnit)
            currentUnit = "metric";
        this.state = { currentUnit: currentUnit, fiveDayForecast: [], isLoading: true,isOnError:false };
    }
    public componentDidMount() {
        navigator.geolocation.getCurrentPosition((arg) => {
            this.getCurrentLocation(arg.coords.latitude, arg.coords.longitude);
        }, (arg) => {
            //We could not get the coordinates from the browser, using the IDB Headquarters as default location;
            this.getCurrentLocation(38.899406, -77.030186);
        });
    }
    //Get the current location from the coordinates
    public getCurrentLocation(latitude: number, longitude: number) {

        fetch(`/api/weather/location?latitude=${latitude}&longitude=${longitude}`).then((response) => response.json()).then(loc => {
            this.setState({
                location: {
                    city: loc.englishName,
                    state: loc.administrativeArea.englishName,
                    country: loc.country.englishName,
                    parentCity: loc.parentCity.englishName,
                    key: loc.key
                }
            });
            this.getCurrentWeather(loc.key);
        }).catch(reason => { this.setState({ isLoading: false, isOnError:true }) });
    }
    //Get the current weather with the location key
    public getCurrentWeather(locationKey: string) {
        fetch(`/api/weather/${locationKey}`).then((response) => {

            if (response.status == 200) {
                response.json().then(curWeather => {
                    this.setState({
                        weatherCondition: {
                            epochTime: curWeather.epochTime,
                            isDayTime: curWeather.isDayTime,
                            relativeHumidity: curWeather.relativeHumidity,
                            temperatureCelcius: curWeather.temperature.metric.value,
                            temperatureFarenheits: curWeather.temperature.imperial.value,
                            weatherIcon: curWeather.weatherIcon,
                            weatherText: curWeather.weatherText,
                            windMetric: `${curWeather.wind.speed.metric.value} ${curWeather.wind.speed.metric.unit}`,
                            windImperial: `${curWeather.wind.speed.imperial.value} ${curWeather.wind.speed.imperial.unit}`
                        }
                    });
                    this.setFiveDayForecast(locationKey, this.state.currentUnit);
                }).catch(reason => { this.setState({ isLoading: false, isOnError: true }) });;
            }
        });
    }
    public setFiveDayForecast(locationKey: string, unit: string) {
        var isMetric = unit == "metric";
        //The forecast API needs one call for metric and one call for imperial;
        //This is storing the values after the first call so only one call for each is made.
        if ((isMetric && this.fiveDayForecastMetric.length == 0) || (!isMetric && this.fiveDayForecastImperial.length == 0)) {
            this.getFiveDayForecast(locationKey, isMetric);
            return;
        }
        this.setState({ fiveDayForecast: isMetric ? this.fiveDayForecastMetric : this.fiveDayForecastImperial });
    }
    public getFiveDayForecast(locationKey: string, isMetric: boolean) {
        this.setState({ isLoading: true });
        fetch(`/api/weather/forecast/${locationKey}?isMetric=${isMetric}`).then((response) => {

            if (response.status == 200) {
                response.json().then(forecast => {
                    var now = moment.default();
                    // If it is after 5pm use the night icon for the forecast of the the current day.
                    var useNight = now.hours() >= 17; 
                    var dailyForecasts: Array<WeatherForecastModel> = forecast.map((dailyWeather: any, index: number) => {
                        return {
                            epochTime: dailyWeather.epochDate,
                            preciptationProbability: useNight && index == 1 ? dailyWeather.night.precipitationProbability : dailyWeather.day.precipitationProbability,
                            weatherIcon: useNight && index == 0 ? dailyWeather.night.icon : dailyWeather.day.icon,
                            maxTemperature: dailyWeather.temperature.maximum.value,
                            minTemperature: dailyWeather.temperature.minimum.value,
                            weatherText: useNight && index == 0 ? dailyWeather.night.iconPhrase : dailyWeather.day.iconPhrase,
                        }
                    });
                    if (isMetric)
                        this.fiveDayForecastMetric = dailyForecasts;
                    else
                        this.fiveDayForecastImperial = dailyForecasts;

                    this.setState({ fiveDayForecast: dailyForecasts, isLoading:false });
                }).catch(reason => { this.setState({ isLoading: false, isOnError: true })});
            }
        });
    }


    public renderLocation(): React.ReactElement {
        if (!this.state.location)
            return <></>;

        return (<h3>{`${this.state.location.city}${this.state.location.parentCity != null && this.state.location.city != this.state.location.parentCity ? `, ${this.state.location.parentCity}` : ''},
                      ${this.state.location.state},
                      ${this.state.location.country}
                      `}</h3>)
    }
    public setCurrentUnit(unit: string) {
        this.cookies.set("currentUnit", unit, { expires: moment.default().add("days", 10).toDate()});
        if (this.state.location)
            this.setFiveDayForecast(this.state.location.key.toString(), unit);
        this.setState({ currentUnit: unit });
    }
    public renderCurrentWeather(): React.ReactElement {
        if (!this.state.weatherCondition) {
            return (<></>)
        }
        return (
            <div>
                <div>
                    <span>{moment.unix(this.state.weatherCondition.epochTime).format("ddd, hh:mm A")}</span>
                </div>
                <div>
                    <span>{this.state.weatherCondition.weatherText}</span>
                </div>
                <div className="row">
                    <div className="col-md-4 col-sm-12 my-auto">
                        <div className="row">
                            <div className="col-6 ">
                                <img src={`images/${this.state.weatherCondition.weatherIcon.toString().length == 1 ? '0' : ''}${this.state.weatherCondition.weatherIcon}-s.png`} />
                            </div>
                            <div className="col-6 my-auto">
                                {this.state.currentUnit == "metric" ?
                                    <span className="fs-4">{Math.round(this.state.weatherCondition.temperatureCelcius)} <sup>&#186;C | <a href="#" className="link-primary" onClick={(evt) => this.setCurrentUnit("imperial")} >&#186;F</a> </sup></span>
                                    :
                                    <span className="fs-4">{this.state.weatherCondition.temperatureFarenheits} <sup>&#186;F | <a href="#" className="link-primary" onClick={(evt) => this.setCurrentUnit("metric")} >&#186;C</a></sup></span>
                                }
                            </div>
                        </div>
                    </div>
                    <div className="col-md-8 col-sm-12">
                        <div className="row weather-info">
                            <div className="col"><span className="fw-bold">Humidity:</span> {this.state.weatherCondition.relativeHumidity}%</div>

                        </div>
                        <div className="row weather-info">
                            <div className="col"><span className="fw-bold">Wind:</span>
                                &nbsp;{this.state.currentUnit == "metric" ? this.state.weatherCondition.windMetric : this.state.weatherCondition.windImperial}
                            </div>

                        </div>
                        <div className="row weather-info">
                            <div className="col"><span className="fw-bold">Precipitaion probability:</span>{this.state.fiveDayForecast.length ? ` ${this.state.fiveDayForecast[0].preciptationProbability}%` : ''}</div>

                        </div>
                    </div>
                </div>
            </div>

        );
    }
    public renderForecast(): React.ReactElement {
        var element = this.state.fiveDayForecast.map((forecast, index) => (
            <div className="col-md col-sm-12 text-center daily-forecast" key={`forecast_${index}`}>
                <div className="row" key={`forecast_day_${index}`}><div className="col text-center">{moment.unix(forecast.epochTime).format("ddd")}</div></div>
                <div className="row"><div className="col text-center"><img src={`images/${forecast.weatherIcon.toString().length == 1 ? '0' : ''}${forecast.weatherIcon}-s.png`} title={forecast.weatherText} /></div></div>
                <div className="row"><div className="col text-center" title="Minimun">{Math.round(forecast.minTemperature)}&#186;</div><div className="col text-center" title="Maximum">{Math.round(forecast.maxTemperature)}&#186;</div></div>
            </div>));
        return <div className="row text-center">{element}</div>;
    }
    public render() {
        var element: React.ReactElement;
        if (!this.state.isLoading && !this.state.isOnError)
            element = (<>{this.renderLocation()}
                {this.renderCurrentWeather()}
                {this.renderForecast()}
            </>);
        else
            if (this.state.isLoading)
                element = <div className="text-center"><img src="images/cloud_load.gif" className="loading-image" role="loading"/></div>
            else
                element = <div className="text-center alert-danger alert" role="error">Error loading the weather information. Please, reload the page.</div>
        return (
            <div className="weather-component row">
                {element}
            </div>
        );
    }
}
export default WeatherForecast;

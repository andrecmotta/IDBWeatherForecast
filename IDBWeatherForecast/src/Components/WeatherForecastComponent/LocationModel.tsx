
interface LocationModel {
    city: string;
    state: string; //This is for the state or any other administrative division, like province.
    country: string;
    parentCity: string;
    key: number; //This key is used to get the weather forecast
}

export default LocationModel;
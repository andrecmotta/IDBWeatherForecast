
interface LocationModel {
    city: string;
    state: string; //This is for the state or any other administrative division, like province.
    country: string;
    parentCity?: string; //Sometimes the city information is a neighboorhod, in this case we can use this information for the city.
    key: number; //This key is used to get the weather forecast
}

export default LocationModel;
# IDB Weather Forecast
## Code Challenge


**Functional requirements:**

- The app shows the browser's current location’s weather conditions including date, location, temperature, corresponding description of the weather, and icon (assume the user will accept geolocation requests from the browser).
- It should show an extended weather forecast for the next 5 days.
- App should show also current humidity, wind, and precipitation percentage (chances of rain).
- User can decide to use Celsius or Fahrenheit and save that selection for future usage (cookie or browser’s local storage).
 

**Non-functional requirements:**
 

- The app supports Chrome 70+ and Microsoft Edge.
- It should be responsive, from small screens (~320 x 570) to desktops.
- TypeScript and C# are preferred over other languages.
- Other libraries/frameworks in both, front-end and back-end are welcome.
- No plugins are necessary to run the app.
- It manages slow responses and errors smoothly.

**Bonus points:**

- Your code is clear, well tabbed, and follows standard naming conventions.
- Code is maintainable.
- Your code is object-oriented and it’s unit tested (not necessarily TDD)
- It runs on .NET Core
- Your components support injection of dependencies and it’s unit tested (not necessarily with TDD)

# Solution
The Weather Forecast APP was implemented using React, Typescript, .NET 6 (Core) with C# and Bootstrap Styles. 
The Weather API used was Accuweather's, available at https://developer.accuweather.com/.


![Desktop](/desktop.png)
![Mobile](/mobile.png)

## Functional Requirements
The app shows browser current location, if the user accept geolocation from the browser and if the information is available. If there is an exception getting the browser's geolocation, the App uses the Interamerican Development Bank's Headquarter's location, in Washington, DC.
The app shows day of week, location, temperature, corresponding description of weather and icon.
It also displays current humidity, wind and precipitation percentage when available.
The user can decide to use Celsius or Fahrenheit (and mi/h or km/h for wind speeds) and the app saves the information in a cookie for future use.

## Non Functional Requirements
The app supports Chrome and Microsoft Edge.
It is responsive, from small screens to desktops, using bootstrap's default CSS classes.
No plugins are necessary to run the app.
A loading screen is shown while the data is being loaded.
Errors of geolocation are treated by using the IDB headquarter's location.
Errors refering to current weather conditions are displayed in a user friendly manner.
Typescript, C#, React, .NET 6 (Core) and Bootstrap were used.
Code is clear, commented out, maintainable, well tabbed and it follows standard naming convetions (Cammel Case).
The project is Object Oriented.






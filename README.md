# Developer Exercise - Airport Listing

This project is developed against the requirements mentioned in the document "Developer Exercise.docx"

This test project is supposed to perform following functions:
 - Download the content using the HttpClient class from the Microsoft.Net.Http NuGet package to download the JSON.
 - The program should get the airports from this JSON feed: https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json
 - Show a list of all the European airports
 - Offer the functionality to filter the list on country.
 - Retrieving the list of the airports should only happen once every 5 minutes.
 - A response header should be used to indicate whether the application got its data from the JSON feed.
 - The web application should be an MVC 5 application.
 - The name of the response header should be ‘from-feed’.

### Extra Points
Create a view that shows the distance between two airports, airports are identified on IATA code. 
 - The application should accept two airports in the URL. 

## Opening the solution:
Once you download the files, you can open the solution in Visual Studio 2015 or above.

Visual Studio solution file path: /DeveloperExercise/Src/AirportsFeedReader.sln

You can compile the application, it should install the packages from NuGet during compilation. Once compiled, you can either debug (F5) or execute the web project directly (Ctrl+F5).

## Testing the output:
Once you run the application, it will open the home page.

[Home Page Image]

This is the start page and it provides the basic navigation options to view two areas.
 - View Listing
 - Calculate Distance
 
### View Airports Listing
Once you click View Listing link, you will be redirected to /airports/ view.
 
[Image]
 
With page load, an ajax request will be made to web-api (/Airports/GetAirportsList) that will return JSON for all airports in Europe. List of airports will be stored on the client side in a KnockoutJS model and filter by country will be done on client side.

You can also test output of the API "/Airports/GetAirportsList" in tools like PostMan in order to test if correct header is returned or not. In the web-view, you can go to developer tool and look at the console log, returned header is displayed in the console.

[Image]

Pager is also added with the airports grid.

[Image]

### Calculate Distance
Once you click Calculate Distance link, you will be redirected to /airports/distance view.

[Image]

This page will show you two drop-downs each listing all airports in europe from the same feed source as used in the listing view.

[Image]

You can choose airports and click the Calculate Distance button, and it will make another ajax call "/airports/calculatedistance?source=[Source]&destination=[Dest]", IATA codes of selected airports will be sent over to the ajax call, that will return the distance between two airports in Kilometers. 0 will be returned in case either of both airports is missing or same airport is selected in both drop-downs.

[Image]

API Url is also displayed so that you can click and view the raw output in the new browser tab.

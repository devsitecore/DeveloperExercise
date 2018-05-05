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


# CITC 1372
## Connor Reed
## Sunrise Sunset V2
### Due: April 9th, 2022
___

This project builds a database with states and their cordinates from an API. It then uses another API that pulls data and shows the user the time the sun rises and sets along with other data. 
___
### View
- There is one main page, that shows the Database contents. Along with 3 other tabbed pages that uses API's to display data. Also a Add City page that adds to DB.
	- The first page is showing the cities that are in the data base. 
	- Once you click on a city it will bring up a tabbed page that links all 3 pages that use an API.
	- There are also two other pages that are for functional purposes that either add or delete cities.

### Control
- API's
    - The first API call will add the long/lat cords to the database.
	- The second one pulls the Sunrise and Sunset times to display.
    - The third one pulls the current conditions to display. 
	- The fourth one pulls Weahter Forecast for the next serval days. 

### Events
- Tapping that shows data or to add/delete data. 
	- One event is for the tap on a seleting city that, calls API for data. 
	- The add and delete city events are simliar and can be accessed from the top bar. 
- Tapping the city will bring a up tabbed page that shows you different conent. 
___
## Other notes: 
- This is not commented as fully as I usually do, as I had to start from scratch completely Friday. The downloaded ZIP from GitHub had dependency issues. 
- I also could not get the grid (last tabbed page) to work within the time I had to play with it and research. I did make it readable, but with alot of empty space. Along with that, I could not figure out how to pull the date next to the name of the day. 
___
[My Portfolio](https://calexreed.me/ "Connor Reeds Portfolio")

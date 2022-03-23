using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using SQLite;

namespace SunriseSunset
{
    public partial class AddCity : ContentPage
    {
        public AddCity()
        {
            InitializeComponent();
        }

        // Creates a list of states for using for the Add city function. Only appears when clicked.
        protected override void OnAppearing()
        {
            base.OnAppearing();

            List<string> states = new List<string>();
            states.Add("Alabama AL");
            states.Add("Alaska AK");
            states.Add("Arizona AZ");
            states.Add("Arkansas AR");
            states.Add("California CA");
            states.Add("Colorado CO");
            states.Add("Connecticut CT");
            states.Add("Delaware DE");
            states.Add("Florida FL");
            states.Add("Georgia GA");
            states.Add("Hawaii HI");
            states.Add("Idaho ID");
            states.Add("Illinois IL");
            states.Add("Indiana IN");
            states.Add("Iowa IA");
            states.Add("Kansas KS");
            states.Add("Kentucky KY");
            states.Add("Louisiana LA");
            states.Add("Maine ME");
            states.Add("Maryland MD");
            states.Add("Massachusetts MA");
            states.Add("Michigan MI");
            states.Add("Minnesota MN");
            states.Add("Mississippi MS");
            states.Add("Missouri MO");
            states.Add("New Jersey NJ");
            states.Add("Montana MT");
            states.Add("Nebraska NE");
            states.Add("Nevada NV");
            states.Add("New Hampshire NH");
            states.Add("New Mexico NM");
            states.Add("New York NY");
            states.Add("North Carolina NC");
            states.Add("North Dakota ND");
            states.Add("Ohio OH");
            states.Add("Oklahoma OK");
            states.Add("Oregon OR");
            states.Add("Pennsylvania PA");
            states.Add("Rhode Island RI");
            states.Add("South Carolina SC");
            states.Add("South Dakota SD");
            states.Add("Tennessee TN");
            states.Add("Texas TX");
            states.Add("Utah UT");
            states.Add("Vermont VT");
            states.Add("Virginia VA");
            states.Add("Washington WA");
            states.Add("West Virginia WV");
            states.Add("Wisconsin WI");
            states.Add("Wyoming WY");

            pkrState.ItemsSource = states;

        } // end of onappearing

        // When submit button is clicked, it will take the information from form to use it below.
        private void btnSubmit_Clicked(object sender, EventArgs e)
        {
            // Takes inputted information from form, and assigns it to strings to put into API call.
            string cityName = efCityName.Text;
            
            string stateName = pkrState.SelectedItem.ToString();
            string country = pkrCountry.SelectedItem.ToString();
          
            ReadAPI(cityName, stateName, country);
           
        } // end of btnSubmit_Clicked

        // With a city, state, and country it will call an API to openweathermap.org to get its cords.
        private async void ReadAPI(string City, string State, string Country)
        {
            // Taking the paramaters and combines them into a single string for the API call.
            string location = City + "," + State + "," + Country;
                                                                 
            string URL = "https://api.openweathermap.org/geo/1.0/direct?q=" + location + "&appid=35b6979e19658b6a31799ba4396ab847"; // just an api key to look at coordinates. Its free, so please get your own

            // Opens a new HTTP client needed for reaching out to the internet??? 
            using(HttpClient client = new HttpClient())
            {

                // try block to see if connection is successful.
                try 
                { 
                    // Talks to client which is the API, and awaits a response.
                    var response = await client.GetAsync(URL);
                    var byteArray = await response.Content.ReadAsByteArrayAsync();

                    string JSON = System.Text.Encoding.Default.GetString(byteArray);

                    // Once the data comes back, it will be converted into a JSON object.
                    List<Location> locations = JsonConvert.DeserializeObject<List<Location>>(JSON);

                    // If locations.Count is less that 0, API unsuccessful.
                    if(locations.Count > 0)
                    {
                        // takes the location of API call, converts to string. 
                        string lat = locations[0].lat.ToString();
                        string lon = locations[0].lon.ToString();
                        string name = locations[0].name;
                        
                        // Will insert the city into database and show a message that it was successful.
                        InsertCity(lat, lon, name);
                        
                        _ = Navigation.PushAsync(new MainPage());
                        
                    } // end of if block

                    // If API was not right, it will return the message below.
                    else
                    {
                        _ = DisplayAlert("Warning", "API could not find city", "OK");
                    } // end of else block
                } // end of if block

                // Exception for try block to catch any unexpected error. Displays message.
                catch(Exception err)
                {
                    _ = DisplayAlert("Error", err.Message, "OK");
                } // end of catch
            } // end of using HTTPClient
        }// end of ReadAPI

        private void InsertCity(string lat, string lon, string cityName)
        {
            // Creates a new city object and assigns the lat, lon, and name to its properties.
            City city = new City(); //{ lat = lat, lon = lon, name = cityName };
            city.lat = lat;
            city.lon = lon;
            city.name = cityName;

            // Uses SQL connection to connect to database.
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //  try block to try to connect to database.
                try
                {
                    // creates table if not created already
                    conn.CreateTable<City>();

                    // Inserts city into database.
                    int rowCount = conn.Insert(city);
                    Navigation.PopAsync();
                } // end of try block

                // Uses catch block to catch any unexpected error.
                catch (Exception err)
                {
                    _ = DisplayAlert("Error", err.Message, "OK");
                } // end of catch block.
                
            } // end of using SQL database
        } // end of InsertCity function.
    } // end of class AddCity
} // end of namespace

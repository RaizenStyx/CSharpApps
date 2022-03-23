
using Xamarin.Forms;
using SQLite;
using System;
using System.Net.Http;
using Newtonsoft.Json;


namespace SunriseSunset
{
    public partial class CityInfo : ContentPage
    {
        
        public CityInfo()
        {
            
            InitializeComponent();

        }

        // overloaded constructor that takes a City to display its info
        public CityInfo(City thisCity)
        {
            InitializeComponent();

            // Makes the lat and lon a string
            string lat = thisCity.lat;
            string lon = thisCity.lon;

            cityName.Text = thisCity.name.ToString();

            // calls ReadAPISunset to retreive info  to display
            ReadAPISunset(thisCity, lat, lon);
            
        }

        // Deletes the city you are currently on.
        private void btnDeleteCity_Clicked(object sender, EventArgs e)
        {
            // Connects to database
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            { 
                // Calls Delete function and removes it from database at the App.city location.
                int count = conn.Delete(App.city);
                if (count > 0)
                {
                    _ = DisplayAlert("Delete", "Success", "OK");
                    Navigation.PopAsync();
                }
                else
                {
                    _ = DisplayAlert("Delete", "Failure", "Ok");
                }
                //conn.Close();
            } 
        }

        // API call to get info to show on city page
        private async void ReadAPISunset(City thisCity, string lat, string lon)
        {
            // Takes the city passed in, gets its lat and lon from database.
            lat = thisCity.lat;
            lon = thisCity.lon;
            
            // Uses sunrise-sunset API to try and retrieve info
            string URL = "https://api.sunrise-sunset.org/json?lat=" + lat + "&lng=" + lon + "&date=today";
                           
            // Opens a client to try and get a connection to get info
            using (HttpClient client = new HttpClient())
            {
                try
                {
                  
                    var response = await client.GetAsync(URL);
                    var byteArray = await response.Content.ReadAsByteArrayAsync();

                    string JSON = System.Text.Encoding.Default.GetString(byteArray);
                  
                    SunriseSunset sunriseset = JsonConvert.DeserializeObject<SunriseSunset>(JSON);

                    // if block to test if the return is nothing, if it has something it will probably be successful
                    if (!String.IsNullOrEmpty(sunriseset.results.sunrise))
                    {
                        // if successful, it will display the text in page with the information retrieved.
                        sunrise.Text = "Sunrise: " + sunriseset.results.sunrise;
                        sunset.Text = "Sunset: " + sunriseset.results.sunset;
                        dayLength.Text = "Day Length: " + sunriseset.results.day_length;
                        startTwilight.Text = "Twilight Begins: " + sunriseset.results.astronomical_twilight_begin;
                        endTwilight.Text = "Twilight Ends: " + sunriseset.results.astronomical_twilight_end;
                    }
                    // if it is null, it will display a warning message.
                    else
                    {
                        _ = DisplayAlert("Warning", "could not find api?", "ok");
                    }

                }
                // catch block to catch unexpected failures.
                catch (Exception ex)
                {
                    _ = DisplayAlert("Error", ex.Message, "OK");
                } // end of catch block
            } // end of using client
        } // end of ReadAPISunset function
    } // end of CityInfo class
} // end of namespace
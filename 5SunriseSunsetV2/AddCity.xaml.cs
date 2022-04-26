using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SunriseSunset
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCity : ContentPage
    {
        private bool success = false;
        public AddCity()
        {
            InitializeComponent();
        }

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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnSubmit_Clicked(object sender, EventArgs e)
        {

            ReadGEOAPIAsync(efCityName.Text, pkrState.SelectedItem.ToString(), pkrCountry.SelectedItem.ToString());

            if (success)
            {
                Navigation.PopAsync();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        private async void ReadGEOAPIAsync(string city, string state, string country)
        {
            string twoCharState = state.Substring(state.Length - 2);
            string twoCharCountry = country.Substring(country.Length - 2);

            using (HttpClient client = new HttpClient())
            {
                string restAPI = "https://api.openweathermap.org/geo/1.0/direct?q=" + city + "," + twoCharState + "," + twoCharCountry + "&limit=5&appid=35b6979e19658b6a31799ba4396ab847";

                var response2 = await client.GetAsync(restAPI);
                string json2 = await response2.Content.ReadAsStringAsync();

                try
                {
                    if (String.IsNullOrEmpty(efCityName.Text))
                    {
                        _ = DisplayAlert("You must enter a city", "warning", "OK");
                        success = false;
                    }
                    else
                    {
                        List<CityLocation> results = JsonConvert.DeserializeObject<List<CityLocation>>(json2);

                        if (results.Count > 0)
                        {
                            //found city
                            var cityName = results[0].name;
                            var stateName = results[0].state;
                            var lon = results[0].lon;
                            var lat = results[0].lat;

                            Location location = new Location();
                            location.state = stateName;
                            location.name = cityName;
                            location.latitude = lat.ToString();
                            location.longtitude = lon.ToString();
                            location.country = stateName;
                            location.stationID = "TBA";

                            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
                            conn.CreateTable<Location>(); //create if it doesn't exist
                            int rowCount = conn.Insert(location);
                            conn.Close();

                            if (rowCount > 0)
                                success = true;
                            else
                                success = false;

                            if (success)
                            {
                                _ = DisplayAlert("New city added!", "Success", "OK");
                                _ = Navigation.PopAsync();
                            }
                            else
                                _ = DisplayAlert("Could not add new city to list.", "Error", "OK");


                        }
                        else
                        {
                            //city not found
                            _ = DisplayAlert("City not found.", "Warning", "OK");
                            success = false;

                        }
                    }

                }
                catch (Exception ex)
                {
                    _ = DisplayAlert(ex.Message, "Could not connect to API", "OK");
                }

                //=============================================================
            } // end using
        }//end ReadGEOAPIAsync
    }//end class AddCity
}//end namespace
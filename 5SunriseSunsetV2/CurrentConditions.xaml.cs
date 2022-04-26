using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SunriseSunset
{
    /// <summary>
    /// Creates a page to use in tabbed page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentConditions : ContentPage
    {
        internal CurrentConditions()
        {
            InitializeComponent();
        }
        // using onAppearing to use API call
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ReadCurrentAPI();
        }

        /// <summary>
        /// Using a API to get current conditions
        /// </summary>
        private async void ReadCurrentAPI()
        {
            // Creates a client to use api
            using(HttpClient client = new HttpClient())
            {
                try // Try block to try to use API
                {
                    // Using JSON from api and deserilizing it
                    var currentResponse = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?lat=" + App.loc.latitude + "&lon=" + App.loc.longtitude + "&appid=35b6979e19658b6a31799ba4396ab847&units=imperial");
               
                    var currentJson = await currentResponse.Content.ReadAsStringAsync();

                    var allResults = JsonConvert.DeserializeObject<CurrentConditionsClass>(currentJson);

                    CurrentCityName.Text = App.loc.name; // App location name for name

                    // Creates a way to use the Icon from API
                    string iconURL = "https://openweathermap.org/img/wn/";
                    string weatherICON = allResults.weather[0].icon + ".png";
                    string iconAddress = iconURL + weatherICON;
                    CurrentCityIcon.Source = iconAddress; // binds to Source

                    // Following is using allResults to set info on XAML
                    CurrentCityConditions.Text = "Conditions: " + allResults.weather[0].main;

                    CurrentCityTemp.Text = "Current Temp: " + allResults.main.temp.ToString();

                    CurrentCityFeelLike.Text = "Feels Like: " + allResults.main.feels_like.ToString();

                    CurrentCityMaxTemp.Text = "Max Temp: " + allResults.main.temp_max.ToString();

                    CurrentCityMinTemp.Text = "Min Temp: " + allResults.main.temp_min.ToString();

                    CurrentCityPressure.Text = "Barometric Pressure: " + allResults.main.pressure.ToString();

                    CurrentCityHumidity.Text = "Humidity: " + allResults.main.humidity.ToString();

                    CurrentCityWindSpeed.Text = "Wind Speed: " + allResults.wind.speed.ToString();

                    CurrentCityWindDegrees.Text = "Wind Degrees: " + allResults.wind.deg.ToString();

                    CurrentCityWindGust.Text = "Wind Gusts: " + allResults.wind.gust.ToString();
                    } // end of try block
                    catch (Exception ex)
                    {
                        _ = DisplayAlert("Error", "No API response - " + ex.Message, "OK");
                    } // catch block to catch any errors
            } // end of using client
        } // End of ReadCurrentAPI
    } // end of Content Page
} // end of namespace
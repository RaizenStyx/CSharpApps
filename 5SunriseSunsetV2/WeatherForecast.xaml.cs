using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SunriseSunset
{
    // Creates a content page to use in tabbed page
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherForecast : ContentPage
    {
        public WeatherForecast()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Reading API call is in onAppearing 
        /// to prevent it from calling all 3 API's at once.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ForecastAPI();
        }
        /// <summary>
        /// API call for weather forecast for next 7 days
        /// </summary>
        private async void ForecastAPI()
        {
            // Opens client connenction
            using (HttpClient client = new HttpClient())
            {
                try // try block to try and load API
                {
                    // New list of DisplayWeather Object 
                    List<DisplayWeather> dailyWeather = new List<DisplayWeather>();
                    // API call and read and Desericalizing
                    var forecastResponse = await client.GetAsync("https://api.openweathermap.org/data/2.5/onecall?lat=" + App.loc.latitude + "&lon=" + App.loc.longtitude + "&exclude=current,minutely,hourly&appid=35b6979e19658b6a31799ba4396ab847&units=imperial");
                
                    var forecastJson = await forecastResponse.Content.ReadAsStringAsync();

                    var forecastResults = JsonConvert.DeserializeObject<Forecast>(forecastJson);
                    // Count to increase each time through for loop for bgColor change
                    int count = 1;
                    
                    // For each loop in forecastResults for every day 
                    foreach(var day in forecastResults.daily)
                    {
                        // temporary DisplayWeahter object to add to displayWeather list of Objects
                        DisplayWeather tempWeather = new DisplayWeather();

                        // Using day.dt and converts it to the right day
                        DateTime localDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(day.dt).DateTime.ToLocalTime();
                        string dayOfWeek = localDateTimeOffset.DayOfWeek.ToString();

                        DateTime dateTime = localDateTimeOffset.ToUniversalTime();

                        if (count % 2 == 0) // if block to switch bgColor every other time
                        {
                            tempWeather.bgColor = "Aquamarine";
                        }
                        else
                        {
                            tempWeather.bgColor = "Aqua";
                        }

                        // sets all attribuites of tempWeather according to which day is coming through loop
                        tempWeather.localDateTimeOffset = dayOfWeek;
                        tempWeather.min = "Low: " + day.temp.min.ToString();
                        tempWeather.max = "High: " + day.temp.max.ToString();
                        tempWeather.humidity = "Humidity: " + day.humidity.ToString();
                        tempWeather.windSpeed = "Wind: " + day.wind_speed.ToString();
                        tempWeather.windDirection = "Dir: " + day.wind_deg.ToString();
                        tempWeather.windGust = "Gust: " + day.wind_gust.ToString();
                        tempWeather.desc = day.weather[0].description;

                        // adds every day that comes through foreach loop to dailyWeather list
                        dailyWeather.Add(tempWeather);

                        count++;
                    }

                    // Once bound in dailyWeather list, it will bind to .xaml
                    lstForecast.ItemsSource = dailyWeather;

                } // end try block
                catch (Exception ex)
                {
                    _ = DisplayAlert("Error", "Didnt update form - " + ex.Message, "OK");
                } // end catch block to show errors if try block is unsuccessful

            } // end of using client
        } // end of ForecastAPI
    } // end of content page
} // end of namespace
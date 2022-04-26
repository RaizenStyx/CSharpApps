using Newtonsoft.Json;
using SQLite;
using System;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SunriseSunset
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SunriseSunset : ContentPage
    {
        internal SunriseSunset()
        {
            InitializeComponent();
            ReadAPIAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void ReadAPIAsync()
        {
            var lat = App.loc.latitude;
            var lng = App.loc.longtitude;
            var name = App.loc.name;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Current Date, API can only do one date at a time, not a range
                    DateTime now = DateTime.Now;
                    string date = now.ToString("yyyy-MM-dd");

                    var response2 = await client.GetAsync("https://api.sunrise-sunset.org/json?lat=" + lat + "&lng=" + lng + "=" + date);
                    var json2 = await response2.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<Sunrises>(json2);

                    lblSunriseLocation.Text = name;

                    DateTime univDateTime = DateTime.Parse(results.results.sunrise);
                    DateTime localDateTime = univDateTime.ToLocalTime();
                    lblSunriseDateTime.Text = "Sunrise: " + localDateTime;

                    univDateTime = DateTime.Parse(results.results.sunset);
                    localDateTime = univDateTime.ToLocalTime();
                    lblSunsetDateTime.Text = "Sunset: " + localDateTime;

                    lblDayLength.Text = "Day Length: " + results.results.day_length;

                    univDateTime = DateTime.Parse(results.results.civil_twilight_begin);
                    localDateTime = univDateTime.ToLocalTime();
                    lbltwilightStart.Text = "Twilight Begins: " + localDateTime;

                    univDateTime = DateTime.Parse(results.results.civil_twilight_end);
                    localDateTime = univDateTime.ToLocalTime();
                    lbltwilightEnd.Text = "Twilight Ends: " + localDateTime;
                }
                catch (Exception ex)
                {
                    _ = DisplayAlert("Error", "No response from API call. - " + ex.Message, "OK");
                }
            } // end using
        }// end ReadAPIAsync

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCity_Clicked(object sender, EventArgs e)
        {
            int rowsDeleted = 0;
            //delete database item
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Location>(); //create if it doesn't exist                
                rowsDeleted = conn.Delete(App.loc);
            }
            //
            if (rowsDeleted > 0)
                _ = DisplayAlert("Success", "Location successfully deleted from the database.", "OK");
            else
                _ = DisplayAlert("Error", "Location could not be found in the database.", "OK");

            Navigation.PopAsync();
        }//end btnDeleteCity_Clicked
    }//end SunriseSunset
}//end namespace
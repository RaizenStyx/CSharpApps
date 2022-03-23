using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SunriseSunset
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // btn that when clicks, navigates to a new page.
        private void btnAddCity_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCity());
        }

        // When a city item is tapped, this will be called and navigates to another page while passing the data for it.
        private void listLocation_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            City loc = (City)e.Item;
            App.city = loc;
            Navigation.PushAsync(new CityInfo(loc));

        }

        // When page loads, it will create a list of cities from the database.
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // creates a new list of type City
            List<City> cities = new List<City>();

            // opens a SQL connection, connecting to the database.
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                // creates table if not created already
                conn.CreateTable<City>();
                cities = conn.Table<City>().ToList();
                 
                // Assigns the listlocation's itemsource to the list of cities.
                listLocation.ItemsSource = cities;
 
            } // end of using DB
        } // end of override OnAppearing
    } // end of MainPage class
} // end of namespace

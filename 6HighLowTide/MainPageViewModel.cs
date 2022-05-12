using High_Low_Tides.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;

/// <summary>
/// Name: Connor Reed
/// File: MainPageViewModel.cs
/// Project: High_Low_Tides
/// Revision Date: 04/03/2022
/// </summary>


namespace High_Low_Tides.ViewModels
{
    internal class MainPageViewModel
    {	// Creating a ObservableCollection to use and bind to XAML
        public ObservableCollection<Prediction> displayPredictions { get; set; }
        
	// Constructor for the viewmodel. Declares the ObservableCollection.
        public MainPageViewModel()
        {
            displayPredictions = new ObservableCollection<Prediction>();
       
        }
	// API call
        public async void PredictionAPI()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
			// API string to use
                    DateTime now = DateTime.Now;
                    string date = now.ToString("MM/dd/yyyy");
                    string start = "https://api.tidesandcurrents.noaa.gov/api/prod/datagetter?station=8726724&time_zone=lst_ldt&units=english&product=predictions&range=120&begin_date=";
                    string end = "&format=json&interval=hilo&product=water_temperature&application=Connor&datum=MLLW";
			// Puts that string in getasync to call the api
                    var response = await client.GetAsync(start+date+end);
                    	// Awaits response, reads in as string
                    var predictionJson = await response.Content.ReadAsStringAsync();
			// Converts the response to Json
                    var predcionResults = JsonConvert.DeserializeObject<Predictions>(predictionJson);
                    	// new list of Predictions
                    List<Prediction> mypredictions = new List<Prediction>();
                    
			// for each loop of type Prediction into the json results. These get added to List
                    foreach (Prediction predict in predcionResults.tidepredictions)
                    {
                        Prediction temp = new Prediction();

                        temp.t = predict.t;
                        temp.type = predict.type;

                        if(temp.type == "H")
                        {
                            temp.type = "High Tide";
                        }
                        else
                        {
                            temp.type = "Low Tide";
                        }

                        mypredictions.Add(temp);

                    } // end of for each
			// for each loop through List, then adds to ObservableCollection
                    foreach (Prediction predic in mypredictions)
                    {
                        displayPredictions.Add(predic);
                    } // end of foreach
                
                } // end of try block
                    catch (Exception ex)
                {
                    _ = Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                } // end of catch
                
            } // end of using
        } // end of PredictionAPI function
    } // end of internal class
} // end of namespace

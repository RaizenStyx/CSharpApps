using High_Low_Tides.ViewModels;
using Xamarin.Forms;

/// <summary>
/// Name: Connor Reed
/// File: MainPage.xaml.cs
/// Project: High_Low_Tides
/// Revision Date: 04/03/2022
/// </summary>

namespace High_Low_Tides
{
    public partial class MainPage : ContentPage
    {	
	// creates resource to use from ViewModel
        private MainPageViewModel vm;
        
	// Inits the mainpage while binding reseource to xaml.
        public MainPage()
        {
            InitializeComponent();
            vm = (MainPageViewModel)Resources["vm"];
        }
	
	// Calls the vm API call onApearing and the base onAppearing as well
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.PredictionAPI();
        } // end of onApearing
    }// end of MainPage Class
} // end of namespace

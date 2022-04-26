
using Xamarin.Forms;


namespace SunriseSunset
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static Location loc;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string dbLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            DatabaseLocation = dbLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

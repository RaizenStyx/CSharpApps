

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SunriseSunset
{
    /// <summary>
    /// Creates a tabbed page to use for placing other content pages on
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityTabbedPage : TabbedPage
    {
        public CityTabbedPage()
        {
            InitializeComponent();
        }
    }
}
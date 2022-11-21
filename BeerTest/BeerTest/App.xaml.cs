using BeerTest.Services;
using BeerTest.Views;
using Xamarin.Forms;

namespace BeerTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            NavigationService.Initialize(MainPage.Navigation);
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
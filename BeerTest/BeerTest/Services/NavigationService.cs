using System;
using System.Threading.Tasks;
using BeerTest.Model;
using BeerTest.Views;
using Xamarin.Forms;

namespace BeerTest.Services
{
    public class NavigationService : INavigationService
    {
        static Lazy<NavigationService> _instance;

        readonly INavigation _navigation;

        protected NavigationService(INavigation navigation)
        {
            _navigation = navigation;
        }

        public async Task Navigate(Beer beer)
        {
            await _navigation.PushAsync(new BeerPage(beer) { Title = beer.Name });
        }

        public static INavigationService Create()
        {
            return _instance.Value;
        }

        public static void Initialize(INavigation navigation)
        {
            _instance ??= new Lazy<NavigationService>(() => new NavigationService(navigation));
        }
    }
}
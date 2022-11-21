using System.Threading.Tasks;
using BeerTest.Model;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace BeerTest.Views
{
    public partial class BeerPage : ContentPage
    {
        readonly BeerPageViewModel _viewModel;

        public BeerPage(Beer beer)
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            
            BindingContext = new BeerPageViewModel(beer);
            
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _ = Task.Run(async () => await _viewModel.OnAppearing());
        }
    }
}
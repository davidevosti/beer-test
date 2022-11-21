using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace BeerTest.Views
{
    public partial class MainPage : ContentPage
    {
        readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            _viewModel = new MainPageViewModel();

            BindingContext = _viewModel;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _ = Task.Run(async () => await _viewModel.OnAppearing());
        }
    }
}
using System.Threading.Tasks;
using BeerTest.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Xamarin.Forms;

namespace BeerTest.Views
{
    public class BeerPageViewModel : ObservableRecipient
    {
        readonly Beer _beer;

        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource ImageSource { get; set; }

        public BeerPageViewModel(Beer beer)
        {
            _beer = beer;

            ImageSource = _beer.ImageSource;
            Name = _beer.Name;
            Description = _beer.Description;
        }

        public Task OnAppearing()
        {
            return Task.CompletedTask;
        }
    }
}
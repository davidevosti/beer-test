using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BeerTest.Model;
using BeerTest.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Xamarin.Forms;

namespace BeerTest.Views
{
    public class MainPageViewModel : ObservableRecipient
    {
        readonly IBeerService _beerService;

        CancellationTokenSource _filteringCancellationToken;

        string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Beers.Clear();
                    return;
                }
                
                if (_searchText != value)
                {
                    try
                    {
                        _filteringCancellationToken?.Cancel();
                    }
                    catch
                    {
                        // ignored
                    }

                    _filteringCancellationToken = new CancellationTokenSource();

                    //don't set .ConfigureAwait(false) because the code execution must continue on the UI thread since
                    _ = Task.Run(async () =>
                        {
                            try
                            {
                                await Task.Delay(TimeSpan.FromSeconds(1));

                                await SearchCommandImplementation(value, _filteringCancellationToken);

                                _filteringCancellationToken = null;
                            }
                            catch (OperationCanceledException)
                            {
                                //ignored
                            }
                        },
                        _filteringCancellationToken.Token);
                }

                SetProperty(ref _searchText, value);
            }
        }

        ObservableCollection<Beer> _beers;

        public ObservableCollection<Beer> Beers
        {
            get => _beers;
            set => SetProperty(ref _beers, value);
        }

        Beer _selectedBeer;

        public Beer SelectedBeer
        {
            get => _selectedBeer;
            set => NavigationService.Create()?.Navigate(value);
        }

        public MainPageViewModel()
        {
            _beerService = BeerService.Create();

            Beers = new ObservableCollection<Beer>(new List<Beer>());

            _filteringCancellationToken = new CancellationTokenSource();
        }

        public Task OnAppearing()
        {
            return Task.CompletedTask;
        }


        public ICommand SearchCommand => new Command(async () => await SearchCommandImplementation(SearchText, _filteringCancellationToken));

        async Task SearchCommandImplementation(string text, CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var beers = await _beerService.Search(text, cancellationTokenSource.Token);

                await Device.InvokeOnMainThreadAsync(() =>
                {
                    //Best would be to merge the results so that only the affected items are refreshed
                    Beers.Clear();
                    foreach (var beer in beers)
                    {
                        Beers.Add(beer);
                    }
                }).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // User cancelled
            }
            catch
            {
                //show an error to the user
            }
        }
    }
}
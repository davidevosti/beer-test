using System.Threading.Tasks;
using BeerTest.Model;

namespace BeerTest.Services
{
    public interface INavigationService
    {
        Task Navigate(Beer beer);
    }
}
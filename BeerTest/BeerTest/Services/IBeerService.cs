using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeerTest.Model;

namespace BeerTest.Services
{
    public interface IBeerService
    {
        Task<IReadOnlyCollection<Beer>> Search(string text, CancellationToken token);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BeerTest.Model;

namespace BeerTest.Services
{
    public class BeerService : IBeerService
    {
        readonly HttpClient _client;

        static readonly Lazy<BeerService> Instance = new Lazy<BeerService>(() => new BeerService());

        protected BeerService()
        {
            _client = new HttpClient(); //I want to use an HttpClient so that I can inject an customized HttpMessageHandler to have the native communication
        }

        public async Task<IReadOnlyCollection<Beer>> Search(string text, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(text);
            }

            cancellationToken.ThrowIfCancellationRequested();

            text = text.Replace(" ", "_");

            try
            {
                var response = await _client.GetStreamAsync($"https://api.punkapi.com/v2/beers?beer_name={text}");
                //  var response = await _client.GetStringAsync($"https://api.punkapi.com/v2/beers?beer_name={text}");

                var rto = await JsonSerializer.DeserializeAsync<IEnumerable<BeerRto>>(response);

                //  var enumerator = JsonSerializer.DeserializeAsyncEnumerable<BeerRto>(response, JsonSerializerOptions.Default, cancellationToken);

                var beers = new List<Beer>();

                // Filter off all beers without an image so that I don't have to deal with a default one :)
                // Of course instead of just skipping I should set a default image...
                foreach (var beer in rto.ToList().Where(p => p.image_url != null))
                {
                    beers.Add(Beer.FromRto(beer));
                }

                return beers;
            }
            catch (Exception ex)
            {
                //log ex
                var message = ex.Message;

                throw;
            }
        }

        public static IBeerService Create()
        {
            return Instance.Value;
        }
    }
}
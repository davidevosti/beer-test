using System;
using Xamarin.Forms;

namespace BeerTest.Model
{
    public class Beer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Uri ImageUrl { get; set; }

        public ImageSource ImageSource => ImageUrl != null ? ImageSource.FromUri(ImageUrl) : null;

        public static Beer FromRto(BeerRto rto)
        {
            if (rto == null)
            {
                throw new ArgumentNullException(nameof(rto));
            }

            var beer = new Beer { Id = rto.id, Name = rto.name, Description = rto.description };

            if (rto.image_url != null)
            {
                try
                {
                    beer.ImageUrl = new Uri(rto.image_url);
                }
                catch
                {
                    //Ignore malformed urls
                }
            }

            return beer;
        }
    }
}
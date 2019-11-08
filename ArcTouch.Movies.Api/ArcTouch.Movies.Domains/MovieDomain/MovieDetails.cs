
using System.Collections.Generic;

namespace ArcTouch.Movies.Domains.MovieDomain
{
    public class MovieDetails
    {
        public MovieDetails(string overview, IEnumerable<Genre> genres)
        {
            Overview = overview;
            Genres = genres;
        }

        public string Overview { get; protected set; }
        public IEnumerable<Genre> Genres { get; protected set; }
    }
}

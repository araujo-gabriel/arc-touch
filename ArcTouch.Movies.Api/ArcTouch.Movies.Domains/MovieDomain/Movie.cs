using ArcTouch.Movies.Domains.Base.Implementations;
using System;

namespace ArcTouch.Movies.Domains.MovieDomain
{
    public class Movie : Entity<int>
    {
        public Movie(int id, string name, string backdrop, DateTime releaseDate)
        {
            Id = id;
            Name = name;
            Backdrop = backdrop;
            ReleaseDate = releaseDate;
        }

        public Movie(int id, string name, string backdrop, DateTime releaseDate,
            MovieDetails details)
        {
            Id = id;
            Name = name;
            Backdrop = backdrop;
            ReleaseDate = releaseDate;
            Details = details;
        }

        public string Name { get; protected set; }

        public DateTime ReleaseDate { get; protected set; }
        public MovieDetails Details { get; protected set; }

        private string _backdrop;
        public string Backdrop
        {

            get
            {
                if (!string.IsNullOrEmpty(_backdrop))
                    return $"https://image.tmdb.org/t/p/w500{_backdrop}";

                return string.Empty;
            }

            set { _backdrop = value; }

        }
    }
}

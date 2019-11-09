using ArcTouch.Movies.Domains.Base.Implementations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArcTouch.Movies.Domains.MovieDomain.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<Paginator<Movie>> Get(short page = 1);

        Task<Movie> Get(int movieId);
    }
}

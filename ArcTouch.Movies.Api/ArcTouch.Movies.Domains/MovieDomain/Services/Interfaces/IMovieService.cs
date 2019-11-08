using ArcTouch.Movies.Domains.MovieDomain.Base.Implementations;
using System.Threading.Tasks;

namespace ArcTouch.Movies.Domains.MovieDomain.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ResponseMessage> GetMovies(short page = 1);
        Task<ResponseMessage> GetMovieWithDetails(int movieId);
    }
}

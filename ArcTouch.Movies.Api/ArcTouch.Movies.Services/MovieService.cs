using System.Threading.Tasks;
using ArcTouch.Movies.Domains.MovieDomain.Base.Implementations;
using ArcTouch.Movies.Domains.MovieDomain.Repositories.Interfaces;
using ArcTouch.Movies.Domains.MovieDomain.Services.Interfaces;

namespace ArcTouch.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<ResponseMessage> GetMovies(short page = 1)
        {
            var movies = await _movieRepository.Get(page);

            return ResponseMessage.Ok(movies);
        }

        public async Task<ResponseMessage> GetMovieWithDetails(int movieId)
        {
            if (movieId == 0)
                return ResponseMessage.BadRequest("Movie id was not found.");

            var movie = await _movieRepository.Get(movieId);

            return ResponseMessage.Ok(movie);
        }
    }
}

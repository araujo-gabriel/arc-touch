using System.Threading.Tasks;
using ArchTouch.Movies.API.Extensions;
using ArcTouch.Movies.Domains.MovieDomain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArchTouch.Movies.API.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet, ResponseCache(Duration = 86400)]
        public async Task<IActionResult> Get([FromQuery]short page = 1) =>
           await _movieService.GetMovies(page)
                .ToControllerResponse();

        [HttpGet("{movieId}/details"), ResponseCache(Duration = 86400)]
        public async Task<IActionResult> Get([FromRoute]int movieId) =>
            await _movieService.GetMovieWithDetails(movieId)
                .ToControllerResponse();
    }
}

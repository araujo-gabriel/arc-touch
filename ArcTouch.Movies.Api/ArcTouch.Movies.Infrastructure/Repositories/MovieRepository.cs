using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ArcTouch.Movies.Domains.Base.Implementations;
using ArcTouch.Movies.Domains.MovieDomain;
using ArcTouch.Movies.Domains.MovieDomain.Repositories.Interfaces;
using ArcTouch.Movies.Infrastructure.Configurations;
using ArcTouch.Movies.Infrastructure.Configurations.JsonConverters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ArcTouch.Movies.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly TMDbConfiguration _tmdbConfiguration;

        public MovieRepository(IHttpClientFactory clientFactory, IOptions<TMDbConfiguration> tmdbConfigurationOptions)
        {
            if (tmdbConfigurationOptions == null || tmdbConfigurationOptions.Value == null)
                throw new InvalidOperationException("TMDbConfigurations not found");

            _clientFactory = clientFactory;
            _tmdbConfiguration = tmdbConfigurationOptions.Value;
        }

        public async Task<Paginator<Movie>> Get(short page = 1)
        {
            var client = _clientFactory.CreateClient(_tmdbConfiguration.HttpClientName);

            var response = await client
                .GetAsync($"movie/upcoming?api_key={_tmdbConfiguration.Key}&language={_tmdbConfiguration.Language}&page={page}");

            var responseString = await response?.Content?.ReadAsStringAsync();

            var paginator = JsonConvert.DeserializeObject<Paginator<Movie>>(responseString,
                new MovieConverter());

            return paginator;
            
        }

        public async Task<Movie> Get(int movieId)
        {
            var client = _clientFactory.CreateClient(_tmdbConfiguration.HttpClientName);

            var response = await client
                .GetAsync($"movie/{movieId}?api_key={_tmdbConfiguration.Key}&language={_tmdbConfiguration.Language}");

            var responseString = await response?.Content?.ReadAsStringAsync();

            var movies = JsonConvert.DeserializeObject<Movie>(responseString,
                new MovieWithDetailsConverter());

            return movies;
        }
    }
}

using ArcTouch.Movies.Domains.MovieDomain.Repositories.Interfaces;
using ArcTouch.Movies.Domains.MovieDomain.Services.Interfaces;
using ArcTouch.Movies.Infrastructure.Configurations;
using ArcTouch.Movies.Infrastructure.Repositories;
using ArcTouch.Movies.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using System;
using System.Net.Http;

namespace ArcTouch.Movies.IOC
{
    public static class DependencyInjectionExtension
    {

        public static void AddDependenciesInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var tmdbConfigurationOption = configuration.GetSection("TMDbConfiguration");

            services.Configure<TMDbConfiguration>(tmdbConfigurationOption);

            services.AddHttpClient(tmdbConfigurationOption?.GetValue<string>("HttpClientName"), client =>
            {
                var url = tmdbConfigurationOption?.GetValue<string>("Url");

                client.BaseAddress = new Uri(url);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(10));

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)));
        }
    }
}

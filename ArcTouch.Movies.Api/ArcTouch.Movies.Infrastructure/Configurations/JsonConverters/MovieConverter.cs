using ArcTouch.Movies.Domains.Base.Implementations;
using ArcTouch.Movies.Domains.MovieDomain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ArcTouch.Movies.Infrastructure.Configurations.JsonConverters
{

    public class MovieConverter : JsonConverter<Paginator<Movie>>
    {
        public override void WriteJson(JsonWriter writer, Paginator<Movie> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Paginator<Movie> ReadJson(JsonReader reader, Type objectType,
            Paginator<Movie> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {

            if (reader.TokenType != JsonToken.StartObject)
                return Paginator<Movie>.Empty();

            var dados = JObject.Load(reader);

            var results = (JArray)dados["results"];

            if (dados == null || results == null || results.Count == 0)
                return Paginator<Movie>.Empty();

            var movies = new List<Movie>(results.Count);

            foreach (var jToken in results)
            {
                var movieId = jToken["id"].Value<int>();
                var title = jToken["title"].Value<string>();
                var backdrop = jToken["backdrop_path"].Value<string>();
                var releaseDate = jToken["release_date"].Value<DateTime>();

                movies.Add(new Movie(movieId, title, backdrop, releaseDate));
            }

            var page = dados["page"].Value<short>();
            var totalPages = dados["total_pages"].Value<short>();

            return new Paginator<Movie>(page, totalPages, movies);

        }
    }
}

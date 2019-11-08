using ArcTouch.Movies.Domains.MovieDomain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ArcTouch.Movies.Infrastructure.Configurations.JsonConverters
{

    public class MovieWithDetailsConverter : JsonConverter<Movie>
    {
        public override void WriteJson(JsonWriter writer, Movie value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Movie ReadJson(JsonReader reader, Type objectType, Movie existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                return null;

            var dados = JObject.Load(reader);

            if (dados == null)
                return null;

            var movieId = dados["id"].Value<int>();
            var title = dados["title"].Value<string>();
            var backdrop = dados["backdrop_path"].Value<string>();
            var releaseDate = dados["release_date"].Value<DateTime>();

            var overview = dados["overview"].Value<string>();

            var genresJtoken = (JArray)dados["genres"];

            var genres = new List<Genre>(genresJtoken?.Count ?? 0);

            foreach (var jToken in genresJtoken)
            {
                var genreId = jToken["id"].Value<int>();
                var genreName = jToken["name"].Value<string>();

                genres.Add(new Genre(genreId, genreName));
            }

            var details = new MovieDetails(overview, genres);

            return new Movie(movieId, title, backdrop, releaseDate, details);
        }
    }
}

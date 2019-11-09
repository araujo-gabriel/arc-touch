namespace ArcTouch.Movies.Domains.MovieDomain
{
    public class Genre
    {
        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }

    }
}

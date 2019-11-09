using System.Collections.Generic;

namespace ArcTouch.Movies.Domains.Base.Implementations
{
    public class Paginator<T>
    {
        public Paginator(short page, short totalPages, IEnumerable<T> data)
        {
            Page = page;
            TotalPages = totalPages;
            Data = data;
        }

        public short Page { get; protected set; }
        public short TotalPages { get; protected set; }
        public IEnumerable<T> Data { get; protected set; }

        public static Paginator<T> Empty()
        {
            return new Paginator<T>(0, 0, new List<T>());
        }

    }
}

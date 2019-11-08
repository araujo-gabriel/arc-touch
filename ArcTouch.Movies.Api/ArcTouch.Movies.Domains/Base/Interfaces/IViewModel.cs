
namespace ArcTouch.Movies.Domains.MovieDomain.Base.Interfaces
{
    public interface IViewModel
    {
        IViewModel ConvertFromEntity(IEntity entity);
    }
}

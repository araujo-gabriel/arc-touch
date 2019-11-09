using System.Threading.Tasks;

namespace ArcTouch.Movies.Domains.MovieDomain.Base.Interfaces
{
    public interface IEntity
    {
        Task<bool> HasValidState();
        Task<string> GetErrorMessage();
        IViewModel ToViewModel<TViewModel>() where TViewModel : IViewModel;
    }
}

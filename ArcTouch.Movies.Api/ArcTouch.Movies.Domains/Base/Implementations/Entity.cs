using ArcTouch.Movies.Domains.MovieDomain.Base.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTouch.Movies.Domains.Base.Implementations
{
    public abstract class Entity<T> : IEntity
    {
        protected Entity() { }

        public T Id { get; protected set; }

        [JsonIgnore]
        public bool IsNew
        {
            get
            {
                return Id.Equals(default(T));
            }
        }

        [JsonIgnore]
        public IValidator Validator { get; set; }

        [JsonIgnore]
        public ValidationResult ValidatorResult { get; protected set; }

        public virtual IViewModel ToViewModel<TViewModel>() where TViewModel : IViewModel
        {
            var type = typeof(TViewModel);

            var viewModel = (TViewModel)Activator.CreateInstance(type);

            return viewModel.ConvertFromEntity(this);
        }

        public virtual async Task<bool> HasValidState()
        {
            ValidatorResult = await Validator?.ValidateAsync(this);

            return ValidatorResult?.IsValid ?? true;
        }

        public virtual async Task<string> GetErrorMessage()
        {
            ValidatorResult = await Validator?.ValidateAsync(this);

            var errors = ValidatorResult?.Errors.Select(e => e.ErrorMessage);

            return string.Join(Environment.NewLine, errors) ?? string.Empty;
        }
    }
}

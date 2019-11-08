using ArcTouch.Movies.Domains.MovieDomain.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ArcTouch.Movies.Domains.MovieDomain.Base.Implementations
{
    public class ResponseMessage
    {
        public object Data { get; protected set; }
        public string Message { get; protected set; }
        public HttpStatusCode HttpStatusCode { get; protected set; }
        public bool IsSuccess
        {
            get
            {
                return HttpStatusCode != HttpStatusCode.InternalServerError && 
                    HttpStatusCode != HttpStatusCode.BadRequest &&
                    HttpStatusCode != HttpStatusCode.NotFound;
            }
        }

        protected ResponseMessage(string message, HttpStatusCode httpStatusCode)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
        }

        protected ResponseMessage(object data, string message, HttpStatusCode httpStatusCode)
        {
            Data = data;
            Message = message;
            HttpStatusCode = httpStatusCode;
        }

        protected ResponseMessage(IEnumerable<object> data, string message, HttpStatusCode httpStatusCode)
        {
            Data = data;
            Message = message;
            HttpStatusCode = httpStatusCode;
        }

        public static ResponseMessage Unauthorized()
        {
            return new ResponseMessage(string.Empty, HttpStatusCode.Unauthorized);
        }


        public static ResponseMessage Ok()
        {
            return new ResponseMessage(string.Empty, HttpStatusCode.OK);
        }

        public static ResponseMessage Ok(object data)
        {
            return new ResponseMessage(data, string.Empty, HttpStatusCode.OK);
        }

        public static ResponseMessage Ok(IEnumerable<object> data)
        {
            return new ResponseMessage(data, string.Empty, HttpStatusCode.OK);
        }

        public static ResponseMessage Ok<TViewModelType>(IEntity data) where TViewModelType : IViewModel
        {
            var viewModel = data?.ToViewModel<TViewModelType>();

            return new ResponseMessage(viewModel, string.Empty, HttpStatusCode.OK);
        }

        public static ResponseMessage Ok<TViewModelType>(IEnumerable<IEntity> data) where TViewModelType : IViewModel
        {
            var viewModelList = data?
                .ToList()
                .ConvertAll(entity => entity.ToViewModel<TViewModelType>());


            return new ResponseMessage(viewModelList, string.Empty, HttpStatusCode.OK);
        }

        public static ResponseMessage NotFound(string message)
        {
            return new ResponseMessage(message, HttpStatusCode.NotFound);
        }

        public static async Task<ResponseMessage> BadRequest(IEntity entity)
        {
            var message = await entity.GetErrorMessage();

            return new ResponseMessage(message, HttpStatusCode.BadRequest);
        }


        public static ResponseMessage BadRequest(string message)
        {
            return new ResponseMessage(message, HttpStatusCode.BadRequest);
        }

        public static ResponseMessage BadRequest(IEnumerable<string> errors)
        {
            var message = string.Join(Environment.NewLine, errors) ?? string.Empty;

            return new ResponseMessage(message, HttpStatusCode.BadRequest);
        }

        public static ResponseMessage Error(string message)
        {
            return new ResponseMessage(message, HttpStatusCode.InternalServerError);
        }
    }
}

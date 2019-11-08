using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ArchTouch.Movies.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var message = "Ops, an error has occurred, try later.";

            var response = context.HttpContext.Response;
            response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            response.ContentType = "application/json";

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(new { Message = message });
        }
    }
}

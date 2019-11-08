using ArcTouch.Movies.Domains.MovieDomain.Base.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ArchTouch.Movies.API.Extensions
{
    public static class ApiResponseExtension
    {
        public static IActionResult ToControllerResponse(this ResponseMessage responseMessage)
        {
            switch (responseMessage.HttpStatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(new { responseMessage.Data });

                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(new { responseMessage.Message });

                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(new { responseMessage.Message });

                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedResult();

                default:
                    return new StatusCodeResult(HttpStatusCode.InternalServerError.GetHashCode());
            }
        }

        public static async Task<IActionResult> ToControllerResponse(this Task<ResponseMessage> responseMessageTask)
        {
            var responseMessage = await responseMessageTask;

            switch (responseMessage.HttpStatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(new { responseMessage.Data });

                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(new { responseMessage.Message });

                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(new { responseMessage.Message });

                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedResult();

                default:
                    return new StatusCodeResult(HttpStatusCode.InternalServerError.GetHashCode());
            }
        }
    }
}

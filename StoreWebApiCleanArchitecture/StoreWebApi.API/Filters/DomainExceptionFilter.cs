using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApi.Application.Common.Exceptions;

namespace StoreWebApi.API.Filters
{
    public class DomainExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundDomainException notFoundException)
            {
                context.Result = new NotFoundObjectResult(new ProblemDetails
                {
                    Title = "Enity not found",
                    Detail = notFoundException.Message
                });
            }

            if (context.Exception is EntityAlreadyExistsException alreadyExistsException)
            {
                context.Result = new NotFoundObjectResult(new ProblemDetails
                {
                    Title = "Enity already exists",
                    Detail = alreadyExistsException.Message
                });
            }
        }
    }
}

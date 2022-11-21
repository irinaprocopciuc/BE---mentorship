using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store_Web_API.Filters.Custom_Exceptions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Store_Web_API.Services
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

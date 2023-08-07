using Ada.Kanban.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ada.Kanban.Api.Filters
{
    public class ErrorHandlingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ErrorHandlingFilter>>();

            if (exception is not AdaKanbanException)
            {
                return;
            }

            if (((AdaKanbanException)exception).ExceptionType == AdaKanbanExceptionType.NotFound)
            {
                context.Result = new NotFoundObjectResult(exception.Message);
            }
            else if (((AdaKanbanException)exception).ExceptionType == AdaKanbanExceptionType.BadRequest)
            {
                context.Result = new BadRequestObjectResult(exception.Message);
            }
            else if (((AdaKanbanException)exception).ExceptionType == AdaKanbanExceptionType.Unauthorized)
            {
                context.Result = new UnauthorizedObjectResult(exception.Message);
            }

            logger.LogError(exception, exception.Message);
        }
    }
}

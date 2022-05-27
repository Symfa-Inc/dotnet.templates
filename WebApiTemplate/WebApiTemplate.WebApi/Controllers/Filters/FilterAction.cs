using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Domain.Errors;

namespace WebApiTemplate.WebApi.Controllers.Filters
{
    public class FilterAction : IActionFilter
    {
        private readonly ILogger<FilterAction> _logger;

        public FilterAction(ILogger<FilterAction> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                string exception = context.Exception.ToString();

                if (context.Exception is CommonException commonException)
                {
                    _logger.LogError(exception);

                    context.Result = new ObjectResult(commonException.ErrorResponse)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    _logger.LogCritical(exception);

                    context.Result = new ObjectResult(context.Exception)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                context.ExceptionHandled = true;
            }
        }
    }
}

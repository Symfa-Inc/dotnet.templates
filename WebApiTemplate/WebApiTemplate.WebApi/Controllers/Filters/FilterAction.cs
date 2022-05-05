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
                if (context.Exception is BaseException)
                {
                    _logger.LogError(context.Exception.ToString());

                    context.Result = new ObjectResult(context.Exception)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    _logger.LogCritical(context.Exception.ToString());

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

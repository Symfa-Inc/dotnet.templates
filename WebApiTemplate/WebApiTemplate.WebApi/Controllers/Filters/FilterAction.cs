using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiTemplate.Application.Error.Interfaces;

namespace WebApiTemplate.WebApi.Controllers.Filters
{
    public class FilterAction : IActionFilter
    {
        private readonly IErrorService _errorService;
        private readonly ILogger<FilterAction> _logger;

        public FilterAction(IErrorService errorService, ILogger<FilterAction> logger)
        {
            _errorService = errorService;
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                _logger.LogError(context.Exception.ToString());
                return;
            }

            if (_errorService.HasErrors)
            {
                context.Result = new BadRequestObjectResult(_errorService);
                return;
            }
        }
    }
}

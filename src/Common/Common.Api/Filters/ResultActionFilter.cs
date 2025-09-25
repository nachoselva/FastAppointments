namespace Common.Api.Filters
{

    using FluentResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    internal class ResultActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is IResultBase result)
            {
                if (result.IsFailed)
                {
                    context.Result = new BadRequestObjectResult(result.Errors);
                    return;
                }

                var resultType = result.GetType();
                if (resultType.IsGenericType && resultType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IResult<>)))
                {
                    dynamic dynamicResult = result;
                    context.Result = new OkObjectResult(dynamicResult.Value);
                    return;
                }

                context.Result = new OkResult();
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Application.Contracts.Common;

namespace WebAPI.Application.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .SelectMany(x => x.Value!.Errors.Select(e => new FieldError
                {
                    Field = x.Key,
                    Message = e.ErrorMessage
                }))
                .ToList();

            var result = OperationResult<object>.Fail(
                message: "Validation failed",
                errors: errors,
                errorCode: "ValidationError"
            );

            context.Result = new BadRequestObjectResult(result);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No-op
    }
}

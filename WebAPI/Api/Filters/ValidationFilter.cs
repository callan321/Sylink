using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Application.Contracts.Common;

namespace WebAPI.Api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .SelectMany(x => x.Value!.Errors.Select(e => new FieldError
                {
                    Field = Enum.TryParse<FieldName>(x.Key, true, out var parsed) ? parsed : FieldName.general,
                    Message = e.ErrorMessage
                }))
                .ToList();

            context.Result = new BadRequestObjectResult(
                OperationResult<Unit>.Fail("Validation failed", errors)
            );

            return;
        }

        await next();
    }
}

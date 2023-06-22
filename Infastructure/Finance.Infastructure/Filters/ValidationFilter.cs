using Finance.Application.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Finance.Infastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var value = context.ModelState.Where(x => x.Value.Errors.Any())
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();


                var response = ResponseDto<NoContentDto>.Fail(HttpStatusCode.BadRequest, value,-1);
                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)response.StatusCode
                };

                return;
            }

            await next();
        }
    }
}

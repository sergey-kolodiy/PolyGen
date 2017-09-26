using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NoiseLab.PolyGen.Samples.GeneratedCode.Infrastructure
{
    public class NullValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var nullArguments = context.ActionArguments
                .Where(a => a.Value == null)
                .ToList();

            if (nullArguments.Any())
            {
                context.Result = new BadRequestObjectResult(
                    new
                    {
                        arguments = nullArguments.Select(a => $"The {a.Key} argument is required.")
                    });
            }
            base.OnActionExecuting(context);
        }
    }
}

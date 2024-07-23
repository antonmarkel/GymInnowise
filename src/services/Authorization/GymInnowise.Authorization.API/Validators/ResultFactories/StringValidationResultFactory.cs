using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using System.Text;

namespace GymInnowise.Authorization.API.Validators.ResultFactories
{
    public class StringValidationResultFactory : IFluentValidationAutoValidationResultFactory
    {
        public IActionResult CreateActionResult(ActionExecutingContext context,
            ValidationProblemDetails? validationProblemDetails)
        {
            return new BadRequestObjectResult(GetErrorsStringFromDetails(validationProblemDetails));
        }

        private static string? GetErrorsStringFromDetails(ValidationProblemDetails? validationProblemDetails)
        {
            if (validationProblemDetails is null)
            {
                return null;
            }

            var errorStringBuilder = new StringBuilder();
            foreach (var errorPair in validationProblemDetails.Errors)
            {
                foreach (var errorLine in errorPair.Value)
                {
                    errorStringBuilder.AppendLine(errorLine);
                }
            }

            return errorStringBuilder.ToString();
        }
    }
}

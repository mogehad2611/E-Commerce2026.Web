using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce2026.Factories
{
    public static class APIResponseFactory
    {
        public static IActionResult GenApiValidaionResponse (ActionContext Context)
        {
            var Errors = Context.ModelState
                        .Where(M => M.Value.Errors.Any())
                        .Select(M => new ValidationError()
                        {
                            Field = M.Key,
                            Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                        });

            var response = new ValidationErrorsToReturn()
            {
                ValidationErrors = Errors
            };

            return new BadRequestObjectResult(response);
        }
    }
}

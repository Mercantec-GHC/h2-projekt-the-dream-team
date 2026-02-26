using FluentValidation.Results;
using H2Projekt.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace H2Projekt.API
{
    public static class Extensions
    {
        public static IDictionary<string, string[]> ToDictionary(this IEnumerable<ValidationFailure> errors)
        {
            return errors.GroupBy(x => x.PropertyName).ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());
        }

        public static ProblemDetails GetProblemDetails(this NonExistentException ex)
        {
            return new ProblemDetails()
            {
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
                Detail = ex.Message
            };
        }

        public static ProblemDetails GetProblemDetails(this DuplicateException ex)
        {
            return new ProblemDetails()
            {
                Title = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Detail = ex.Message
            };
        }
    }
}

using H2Projekt.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace H2Projekt.API.Extensions
{
    public static class ExceptionExtensions
    {
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

        public static ProblemDetails GetProblemDetails(this InvalidCredentialException ex)
        {
            return new ProblemDetails()
            {
                Title = "Unauthorized",
                Status = StatusCodes.Status401Unauthorized,
                Detail = ex.Message
            };
        }
    }
}

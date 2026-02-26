using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace H2Projekt.Web.Helpers
{
    public static class ApiExceptionHelper
    {
        public static List<ParsedError> ParseErrors(ApiException ex)
        {
            var validationErrors = TryParseValidationProblemDetails(ex.Response);

            if (validationErrors.Any())
            {
                return validationErrors.Select(error => new ParsedError(error, ex.StatusCode)).ToList();
            }

            var problemDetails = TryParseProblemDetails(ex.Response);

            if (problemDetails is not null)
            {
                return new List<ParsedError>()
                {
                    new ParsedError(problemDetails.Detail, ex.StatusCode)
                };
            }

            return new List<ParsedError>()
            {
                new ParsedError(ex.Response, ex.StatusCode)
            };
        }

        private static List<string> TryParseValidationProblemDetails(string response)
        {
            try
            {
                var json = JsonSerializer.Deserialize<ValidationProblemDetails>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });

                if (json is null)
                {
                    return new List<string>();
                }

                return json.Errors.SelectMany(kvp => kvp.Value.Select(error => $"{kvp.Key}: {error}")).ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        private static ProblemDetails? TryParseProblemDetails(string response)
        {
            try
            {
                var json = JsonSerializer.Deserialize<ProblemDetails>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });

                return json;
            }
            catch
            {
                return null;
            }
        }
    }

    public class ParsedError
    {
        public string Message { get; set; }
        public string Severity { get; set; }

        public ParsedError(string message, int statusCode)
        {
            Message = message;
            Severity = statusCode switch
            {
                >= 500 => "danger",
                >= 400 => "warning",
                >= 200 => "success",
                _ => "info"
            };
        }
    }
}

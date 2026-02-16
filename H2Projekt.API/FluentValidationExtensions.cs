using FluentValidation.Results;

namespace H2Projekt.API
{
    public static class FluentValidationExtensions
    {
        public static IDictionary<string, string[]> ToDictionary(this IEnumerable<ValidationFailure> errors)
        {
            return errors.GroupBy(x => x.PropertyName).ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());
        }
    }
}

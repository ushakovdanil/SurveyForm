using Api.Helpers;
using Api.Validators.Abstract;

namespace Api.Validators
{
    public class AgeValidator : IValidator<string>
    {
        public bool Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            var match = RegexUtils.AgeRegex.Match(value);

            return match.Success;
        }
    }
}

using Api.Helpers;
using Api.Validators.Abstract;

namespace Api.Validators
{
    public class NameValidator : IValidator<string>
    {
        public bool Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            var match = RegexUtils.NameRegex.Match(value);

            return match.Success;
        }
    }
}

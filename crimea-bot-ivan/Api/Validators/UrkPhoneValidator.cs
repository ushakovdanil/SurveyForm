using Api.Helpers;
using Api.Validators.Abstract;

namespace Api.Validators
{
    public class UrkPhoneValidator : IValidator<string>
    {
        public bool Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            var match = RegexUtils.UkrPhoneRegex.Match(value);

            return match.Success;
        }
    }
}

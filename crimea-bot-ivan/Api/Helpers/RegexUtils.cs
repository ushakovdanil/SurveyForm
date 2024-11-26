using System.Text.RegularExpressions;

namespace Api.Helpers
{
    public static class RegexUtils
    {
        public static Regex NameRegex { get; } = new Regex(@"([А-ЯІ]{1}[\.][\s]?[А-ЯІ]{1}[\.][ ]?[А-ЯІЇЄA-Z]{1}[а-яіїєa]{1,30})|([А-ЯІЇЄA-Z]{1}[а-яіїєa]{1,30}[ ]?[А-ЯІ]{1}[\.][\s]?[А-ЯІ]{1}[\.])|[А-ЯІЇЄA-Z][а-яіїєa-z\'\-ʼ]+ [А-ЯІЇЄA-Z][а-яіїєa-z\'\-ʼ]+ [А-ЯІЇЄA-Z][а-яіїєa-z'-ʼ]+|[А-ЯІЇЄA-Z][а-яіїєa-z'-ʼ]+ [А-ЯІЇЄA-Z][а-яіїєa-z'-ʼ]+|[А-ЯІЇЄA-Z][а-яіїєa-z'-ʼ]+ \p{Lu}[\.]?[\s]?\p{Lu}[\.]?", RegexOptions.Compiled);

        public static Regex UkrPhoneRegex { get; } = new Regex(@"(\+?38[\-|\.]?(\s)?)?(0)?[\s]?\(?(50|66|95|99|67|68|96|97|98|63|73|93|91|92|94|44|45|31|32|33|34|35|36|37|38|41|43|46|47|48|51|52|53|56|57|61|64)\)?(\d{2,3})?[\-|\.|\/]?(\s)?(\d{2,3})[\-|\.|\/]?(\s)?(\d{2,3})[\-|\.|\/]?(\s)?(\d{2,3})", RegexOptions.Compiled);

        public static Regex AgeRegex { get; } = new Regex(@"^(?:0|[1-9]\d?|100)$");

    }
}

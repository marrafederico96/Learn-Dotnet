using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace FriendStuff.Services;

public partial class NormalizeUrlString
{
    [GeneratedRegex(@"[^a-z0-9\s-]")]
    private static partial Regex NonAlphaNumericRegex();

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    [GeneratedRegex(@"-+")]
    private static partial Regex MultipleHyphensRegex();

    public static string GenerateSlug(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        input = input.ToLowerInvariant();
        input = RemoveDiacritics(input);

        input = NonAlphaNumericRegex().Replace(input, "");
        input = WhitespaceRegex().Replace(input, "-").Trim();
        input = MultipleHyphensRegex().Replace(input, "-");

        return input;
    }

    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in from c in normalizedString let unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c) where unicodeCategory != UnicodeCategory.NonSpacingMark select c)
        {
            stringBuilder.Append(c);
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
using System.Globalization;
using System.Text;

namespace JsonConstantsGenerator.Helpers;

/// <summary>
/// Helper for string normalization.
/// </summary>
public static class StringNormalization
{
    /// <summary>
    /// Removes diacritics (accents, umlaute, ...) from given string.
    /// </summary>
    /// <example>Input "éxämple" would result in "example"</example>
    /// <returns>String without diacritics.</returns>
    public static string RemoveDiacritics(string input)
    {
        string normalized = input.Normalize(NormalizationForm.FormD);
        StringBuilder builder = new StringBuilder();

        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        return builder.ToString();
    }
}
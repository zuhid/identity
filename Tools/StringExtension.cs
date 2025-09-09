using System.Text;
using System.Text.RegularExpressions;

namespace Zuhid.Tools;

public static class StringExtension
{
    /// <summary>
    /// Converts the first character of the input string to lowercase while keeping the remaining characters unchanged.
    /// </summary>
    /// <param name="str">The input string to convert.</param>
    /// <returns>A new string with the first character in lowercase and the rest of the string unchanged.</returns>
    public static string ToCamelCase(this string str)
    {
        return $"{str[..1].ToLower()}{str[1..]}";
    }

    /// <summary>
    /// Converts the first character of the input string to uppercase while keeping the remaining characters unchanged.
    /// </summary>
    /// <param name="str">The input string to convert.</param>
    /// <returns>A new string with the first character in uppercase and the rest of the string unchanged.</returns>
    public static string ToTitleCase(this string str)
    {
        return $"{str[..1].ToUpper()}{str[1..]}";
    }

    /// <summary>
    /// Removes all special characters from the given string, keeping only alphanumeric characters.
    /// </summary>
    /// <param name="str">The input string from which special characters will be removed.</param>
    /// <returns>A new string containing only alphanumeric characters from the input.</returns>
    public static string RemoveSpecialCharacters(this string str)
    {
        var stringBuilder = new StringBuilder();
        foreach (var c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            {
                _ = stringBuilder.Append(c);
            }
        }
        return stringBuilder.ToString();
    }

    public static string ToKebabCase(this string str)
    {
        var result = Regex.Replace(str, "([A-Z][a-z]|(?<=[a-z])[^a-z]|(?<=[A-Z])[0-9_])", "-$1").ToLower();
        return result.StartsWith('-') ? result[1..] : result;
    }


    public static string ToSnakeCase(this string str)
    {
        var result = Regex.Replace(str, "([A-Z][a-z]|(?<=[a-z])[^a-z]|(?<=[A-Z])[0-9_])", "_$1").ToLower();
        return result.StartsWith('_') ? result[1..] : result;
    }

}
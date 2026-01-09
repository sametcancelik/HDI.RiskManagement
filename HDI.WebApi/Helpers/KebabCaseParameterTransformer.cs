using System.Text;
using System.Text.RegularExpressions;

namespace HDI.WebAPI.Helpers;

public class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null) return null;

        string str = value.ToString()!;

        str = str.Replace("İ", "i").Replace("I", "i")
                 .Replace("Ğ", "g").Replace("ğ", "g")
                 .Replace("Ü", "u").Replace("ü", "u")
                 .Replace("Ş", "s").Replace("ş", "s")
                 .Replace("Ö", "o").Replace("ö", "o")
                 .Replace("Ç", "c").Replace("ç", "c");

        return Regex.Replace(str, "([a-z0-9])([A-Z])", "$1-$2").ToLower();
    }
}
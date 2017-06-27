using System;
using System.Text.RegularExpressions;

namespace NoiseLab.Common.Extensions
{
    public static class RegexExtensions
    {
        public static void ThrowIfDoesNotMatch(this Regex regex, string value, string paramName)
        {
            if (!regex.IsMatch(value))
            {
                throw new ArgumentException($"{paramName} does not match \"{regex}\" pattern.", paramName);
            }
        }
    }
}

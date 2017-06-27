using System;

namespace NoiseLab.Common.Extensions
{
    public static class StringExtensions
    {
        public static void ThrowIfNullOrWhitespace(this string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{paramName} cannot be the empty string or all whitespace.", paramName);
            }
        }
    }
}
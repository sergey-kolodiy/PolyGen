using System;

namespace NoiseLab.Common.Extensions
{
    public static class ArrayExtensions
    {
        public static void ThrowIfNullOrEmpty(this object[] source, string paramName)
        {
            source.ThrowIfNull(paramName);
            if (source.Length == 0)
            {
                throw new ArgumentException($"{paramName} cannot be empty.", paramName);
            }
        }

        public static void ThrowIfNullOrEmpty(this string[] source, string paramName)
        {
            source.ThrowIfNull(paramName);
            if (source.Length == 0)
            {
                throw new ArgumentException($"{paramName} cannot be empty.", paramName);
            }
        }
    }
}

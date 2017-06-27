using System;

namespace NoiseLab.Common.Extensions
{
    public static class Int32Extensions
    {
        public static void ThrowIfLessThan(this int value, int minValue, string paramName)
        {
            if (value < minValue)
            {
                throw new ArgumentException($"{paramName} cannot be less than {minValue}.", paramName);
            }
        }

        public static void ThrowIfLessThan(this int? value, int minValue, string paramName)
        {
            if (value.HasValue && value.Value < minValue)
            {
                throw new ArgumentException($"{paramName} cannot be less than {minValue}.", paramName);
            }
        }
    }
}
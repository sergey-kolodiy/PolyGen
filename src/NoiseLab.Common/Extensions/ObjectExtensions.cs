using System;

namespace NoiseLab.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static void ThrowIfNull(this object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} cannot be null.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace NoiseLab.Common.Extensions
{
    public static class ListExtensions
    {
        public static void ThrowIfNullOrEmpty<T>(this List<T> source, string paramName)
        {
            source.ThrowIfNull(paramName);
            if (source.Count == 0)
            {
                throw new ArgumentException($"{paramName} cannot be empty.", paramName);
            }
        }

        public static void ThrowIfDoesNotContain<T>(this List<T> source, T value, string paramName)
        {
            if (!source.Contains(value))
            {
                throw new ArgumentException($"Specified {paramName} = {value} is not supported.", paramName);
            }
        }

        public static void ThrowIfDoesNotContain<T>(this IReadOnlyList<T> source, T value, string paramName)
        {
            if (!source.Contains(value))
            {
                throw new ArgumentException($"Specified {paramName} = {value} is not supported.", paramName);
            }
        }
    }
}
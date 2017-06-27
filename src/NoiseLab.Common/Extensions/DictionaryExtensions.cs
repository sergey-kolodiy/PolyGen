using System;
using System.Collections.Generic;

namespace NoiseLab.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void ThrowIfDoesNotContainKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key, string paramName)
        {
            if (!source.ContainsKey(key))
            {
                throw new ArgumentException($"Specified {paramName} = {key} is not supported.", paramName);
            }
        }
    }
}

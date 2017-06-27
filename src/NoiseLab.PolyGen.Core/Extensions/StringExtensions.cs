using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace NoiseLab.PolyGen.Core.Extensions
{
    internal static class StringExtensions
    {
        internal static string GenerateVerbatimIdentifierString(this string value)
        {
            return $"@{value}";
        }

        internal static SyntaxToken GenerateVerbatimIdentifier(this string value)
        {
            return SyntaxFactory.VerbatimIdentifier(SyntaxTriviaList.Empty, value, value, SyntaxTriviaList.Empty);
        }
    }
}

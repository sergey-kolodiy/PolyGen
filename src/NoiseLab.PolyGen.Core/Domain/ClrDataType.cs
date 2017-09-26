using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NoiseLab.PolyGen.Core.Domain
{
    internal sealed class ClrDataType
    {
        public static ClrDataType Byte { get; } = new ClrDataType("System.Byte", false);
        public static ClrDataType Int16 { get; } = new ClrDataType("System.Int16", false);
        public static ClrDataType Int32 { get; } = new ClrDataType("System.Int32", false);
        public static ClrDataType Int64 { get; } = new ClrDataType("System.Int64", false);
        public static ClrDataType Boolean { get; } = new ClrDataType("System.Boolean", false);
        public static ClrDataType Char { get; } = new ClrDataType("System.Char", false);
        public static ClrDataType DateTime { get; } = new ClrDataType("System.DateTime", false);
        public static ClrDataType DateTimeOffset { get; } = new ClrDataType("System.DateTimeOffset", false);
        public static ClrDataType Guid { get; } = new ClrDataType("System.Guid", false);
        public static ClrDataType TimeSpan { get; } = new ClrDataType("System.TimeSpan", false);
        public static ClrDataType Single { get; } = new ClrDataType("System.Single", false);
        public static ClrDataType Double { get; } = new ClrDataType("System.Double", false);
        public static ClrDataType Decimal { get; } = new ClrDataType("System.Decimal", false);
        public static ClrDataType NullableByte { get; } = new ClrDataType($"System.Nullable<{Byte._name}>", true);
        public static ClrDataType NullableInt16 { get; } = new ClrDataType($"System.Nullable<{Int16._name}>", true);
        public static ClrDataType NullableInt32 { get; } = new ClrDataType($"System.Nullable<{Int32._name}>", true);
        public static ClrDataType NullableInt64 { get; } = new ClrDataType($"System.Nullable<{Int64._name}>", true);
        public static ClrDataType NullableBoolean { get; } = new ClrDataType($"System.Nullable<{Boolean._name}>", true);
        public static ClrDataType NullableChar { get; } = new ClrDataType($"System.Nullable<{Char._name}>", true);
        public static ClrDataType NullableDateTime { get; } = new ClrDataType($"System.Nullable<{DateTime._name}>", true);
        public static ClrDataType NullableDateTimeOffset { get; } = new ClrDataType($"System.Nullable<{DateTimeOffset._name}>", true);
        public static ClrDataType NullableGuid { get; } = new ClrDataType($"System.Nullable<{Guid._name}>", true);
        public static ClrDataType NullableTimeSpan { get; } = new ClrDataType($"System.Nullable<{TimeSpan._name}>", true);
        public static ClrDataType NullableSingle { get; } = new ClrDataType($"System.Nullable<{Single._name}>", true);
        public static ClrDataType NullableDouble { get; } = new ClrDataType($"System.Nullable<{Double._name}>", true);
        public static ClrDataType NullableDecimal { get; } = new ClrDataType($"System.Nullable<{Decimal._name}>", true);
        public static ClrDataType ByteArray { get; } = new ClrDataType("System.Byte[]", false);
        public static ClrDataType String { get; } = new ClrDataType("System.String", false);

        internal TypeSyntax GenerateTypeSyntax()
        {
            return SyntaxFactory.ParseTypeName(_name);
        }

        internal string GenerateValueInvocation()
        {
            return _nullable ? ".Value" : string.Empty;
        }

        private ClrDataType(string name, bool nullable)
        {
            _name = name;
            _nullable = nullable;
        }

        private readonly string _name;
        private readonly bool _nullable;
    }
}
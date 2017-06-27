namespace NoiseLab.PolyGen.Core.Database
{
    public sealed class AbstractDataType
    {
        public static AbstractDataType Byte { get; } = new AbstractDataType("Byte", false, true, ClrDataType.Byte, ClrDataType.NullableByte);
        public static AbstractDataType Int16 { get; } = new AbstractDataType("Int16", false, true, ClrDataType.Int16, ClrDataType.NullableInt16);
        public static AbstractDataType Int32 { get; } = new AbstractDataType("Int32", false, true, ClrDataType.Int32, ClrDataType.NullableInt32);
        public static AbstractDataType Int64 { get; } = new AbstractDataType("Int64", false, true, ClrDataType.Int64, ClrDataType.NullableInt64);
        public static AbstractDataType Guid { get; } = new AbstractDataType("Guid", false, false, ClrDataType.Guid, ClrDataType.NullableGuid);
        public static AbstractDataType Boolean { get; } = new AbstractDataType("Boolean", false, false, ClrDataType.Boolean, ClrDataType.NullableBoolean);
        // TODO: Use a more corresponding ORM data type for this abstract type.
        public static AbstractDataType Date { get; } = new AbstractDataType("Date", false, false, ClrDataType.DateTime, ClrDataType.NullableDateTime);
        public static AbstractDataType DateTime { get; } = new AbstractDataType("DateTime", false, false, ClrDataType.DateTime, ClrDataType.NullableDateTime);
        public static AbstractDataType DateTimeOffset { get; } = new AbstractDataType("DateTimeOffset", false, false, ClrDataType.DateTimeOffset, ClrDataType.NullableDateTimeOffset);
        // TODO: Use a more corresponding ORM data type for this abstract type.
        public static AbstractDataType TimeOfDay { get; } = new AbstractDataType("TimeOfDay", false, false, ClrDataType.TimeSpan, ClrDataType.NullableTimeSpan);
        public static AbstractDataType TimeSpan { get; } = new AbstractDataType("TimeSpan", false, false, ClrDataType.TimeSpan, ClrDataType.NullableTimeSpan);
        public static AbstractDataType Char { get; } = new AbstractDataType("Char", false, false, ClrDataType.Char, ClrDataType.NullableChar);
        public static AbstractDataType Single { get; } = new AbstractDataType("Single", false, true, ClrDataType.Single, ClrDataType.NullableSingle);
        public static AbstractDataType Double { get; } = new AbstractDataType("Double", false, true, ClrDataType.Double, ClrDataType.NullableDouble);
        public static AbstractDataType Decimal { get; } = new AbstractDataType("Decimal", false, true, ClrDataType.Decimal, ClrDataType.NullableDecimal);
        public static AbstractDataType String { get; } = new AbstractDataType("String", true, false, ClrDataType.String, ClrDataType.String);
        public static AbstractDataType Text { get; } = new AbstractDataType("Text", false, false, ClrDataType.String, ClrDataType.String);
        public static AbstractDataType Json { get; } = new AbstractDataType("Json", false, false, ClrDataType.String, ClrDataType.String);
        public static AbstractDataType Xml { get; } = new AbstractDataType("Xml", false, false, ClrDataType.String, ClrDataType.String);
        public static AbstractDataType Binary { get; } = new AbstractDataType("Binary", true, false, ClrDataType.ByteArray, ClrDataType.ByteArray);
        public static AbstractDataType Image { get; } = new AbstractDataType("Image", false, false, ClrDataType.ByteArray, ClrDataType.ByteArray);
        public static AbstractDataType RowVersion { get; } = new AbstractDataType("RowVersion", false, false, ClrDataType.ByteArray, ClrDataType.ByteArray);

        internal ClrDataType GetClrDataType(bool nullable)
        {
            return nullable ? _nullableClrType : _clrType;
        }

        internal string Name { get; }
        internal bool SupportsMaxLengthRestriction { get; }
        public bool SupportsIdentitySpecification { get; }

        private AbstractDataType(string name, bool supportsMaxLengthRestriction, bool supportsIdentitySpecification, ClrDataType clrType, ClrDataType nullableClrType)
        {
            Name = name;
            SupportsMaxLengthRestriction = supportsMaxLengthRestriction;
            SupportsIdentitySpecification = supportsIdentitySpecification;
            _clrType = clrType;
            _nullableClrType = nullableClrType;
        }

        private readonly ClrDataType _clrType;
        private readonly ClrDataType _nullableClrType;
    }
}

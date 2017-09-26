namespace NoiseLab.PolyGen.Core.Domain
{
    public sealed class AbstractDataType
    {
        public override string ToString()
        {
            return _name;
        }

        public static AbstractDataType Byte { get; } = new AbstractDataType("Byte", ClrDataType.Byte, ClrDataType.NullableByte);
        public static AbstractDataType Int16 { get; } = new AbstractDataType("Int16", ClrDataType.Int16, ClrDataType.NullableInt16);
        public static AbstractDataType Int32 { get; } = new AbstractDataType("Int32", ClrDataType.Int32, ClrDataType.NullableInt32);
        public static AbstractDataType Int64 { get; } = new AbstractDataType("Int64", ClrDataType.Int64, ClrDataType.NullableInt64);
        public static AbstractDataType Guid { get; } = new AbstractDataType("Guid", ClrDataType.Guid, ClrDataType.NullableGuid);
        public static AbstractDataType Boolean { get; } = new AbstractDataType("Boolean", ClrDataType.Boolean, ClrDataType.NullableBoolean);
        // TODO: Use a more corresponding ORM data type for this abstract type.
        public static AbstractDataType Date { get; } = new AbstractDataType("Date", ClrDataType.DateTime, ClrDataType.NullableDateTime);
        public static AbstractDataType DateTime { get; } = new AbstractDataType("DateTime", ClrDataType.DateTime, ClrDataType.NullableDateTime);
        public static AbstractDataType DateTimeOffset { get; } = new AbstractDataType("DateTimeOffset", ClrDataType.DateTimeOffset, ClrDataType.NullableDateTimeOffset);
        // TODO: Use a more corresponding ORM data type for this abstract type.
        public static AbstractDataType TimeOfDay { get; } = new AbstractDataType("TimeOfDay", ClrDataType.TimeSpan, ClrDataType.NullableTimeSpan);
        public static AbstractDataType TimeSpan { get; } = new AbstractDataType("TimeSpan", ClrDataType.TimeSpan, ClrDataType.NullableTimeSpan);
        public static AbstractDataType Char { get; } = new AbstractDataType("Char", ClrDataType.Char, ClrDataType.NullableChar);
        public static AbstractDataType Single { get; } = new AbstractDataType("Single", ClrDataType.Single, ClrDataType.NullableSingle);
        public static AbstractDataType Double { get; } = new AbstractDataType("Double", ClrDataType.Double, ClrDataType.NullableDouble);
        public static AbstractDataType Decimal { get; } = new AbstractDataType("Decimal", ClrDataType.Decimal, ClrDataType.NullableDecimal);
        public static AbstractDataType String { get; } = new AbstractDataType("String", ClrDataType.String, ClrDataType.String);
        public static AbstractDataType Text { get; } = new AbstractDataType("Text", ClrDataType.String, ClrDataType.String);
        public static AbstractDataType Json { get; } = new AbstractDataType("Json", ClrDataType.String, ClrDataType.String);
        public static AbstractDataType Xml { get; } = new AbstractDataType("Xml", ClrDataType.String, ClrDataType.String);
        public static AbstractDataType Binary { get; } = new AbstractDataType("Binary", ClrDataType.ByteArray, ClrDataType.ByteArray);
        public static AbstractDataType Image { get; } = new AbstractDataType("Image", ClrDataType.ByteArray, ClrDataType.ByteArray);
        public static AbstractDataType RowVersion { get; } = new AbstractDataType("RowVersion", ClrDataType.ByteArray, ClrDataType.ByteArray);

        internal ClrDataType GetClrDataType(bool nullable)
        {
            return nullable ? _nullableClrType : _clrType;
        }

        private AbstractDataType(string name, ClrDataType clrType, ClrDataType nullableClrType)
        {
            _name = name;
            _clrType = clrType;
            _nullableClrType = nullableClrType;
        }

        private readonly string _name;
        private readonly ClrDataType _clrType;
        private readonly ClrDataType _nullableClrType;
    }
}

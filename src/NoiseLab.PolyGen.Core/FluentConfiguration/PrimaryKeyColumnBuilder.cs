using NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public class PrimaryKeyColumnBuilder : ColumnBuilderBase
    {
        public PrimaryKeyByteColumnSpecificationBuilder Byte()
        {
            var builder = new PrimaryKeyByteColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyInt16ColumnSpecificationBuilder Int16()
        {
            var builder = new PrimaryKeyInt16ColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyInt32ColumnSpecificationBuilder Int32()
        {
            var builder = new PrimaryKeyInt32ColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyInt64ColumnSpecificationBuilder Int64()
        {
            var builder = new PrimaryKeyInt64ColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyGuidColumnSpecificationBuilder Guid()
        {
            var builder = new PrimaryKeyGuidColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyBooleanColumnSpecificationBuilder Boolean()
        {
            var boolean = new PrimaryKeyBooleanColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = boolean;
            return boolean;
        }

        public PrimaryKeyDateColumnSpecificationBuilder Date()
        {
            var builder = new PrimaryKeyDateColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyDateTimeColumnSpecificationBuilder DateTime()
        {
            var builder = new PrimaryKeyDateTimeColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyDateTimeOffsetColumnSpecificationBuilder DateTimeOffset()
        {
            var builder = new PrimaryKeyDateTimeOffsetColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyTimeOfDayColumnSpecificationBuilder TimeOfDay()
        {
            var builder = new PrimaryKeyTimeOfDayColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyTimeSpanColumnSpecificationBuilder TimeSpan()
        {
            var builder = new PrimaryKeyTimeSpanColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyCharColumnSpecificationBuilder Char()
        {
            var builder = new PrimaryKeyCharColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeySingleColumnSpecificationBuilder Single()
        {
            var builder = new PrimaryKeySingleColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyDoubleColumnSpecificationBuilder Double()
        {
            var builder = new PrimaryKeyDoubleColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyDecimalColumnSpecificationBuilder Decimal()
        {
            var builder = new PrimaryKeyDecimalColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyStringColumnSpecificationBuilder String()
        {
            var builder = new PrimaryKeyStringColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyBinaryColumnSpecificationBuilder Binary()
        {
            var builder = new PrimaryKeyBinaryColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyColumnBuilder(TableBuilder tableBuilder, int ordinal, string name)
            : base(tableBuilder, ordinal, name)
        {
        }

        internal override bool IsPrimaryKey => true;
    }
}
using NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public class ColumnBuilder : ColumnBuilderBase
    {
        public ByteColumnSpecificationBuilder Byte()
        {
            var builder = new ByteColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public Int16ColumnSpecificationBuilder Int16()
        {
            var builder = new Int16ColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public Int32ColumnSpecificationBuilder Int32()
        {
            var builder = new Int32ColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public Int64ColumnSpecificationBuilder Int64()
        {
            var builder = new Int64ColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public GuidColumnSpecificationBuilder Guid()
        {
            var builder = new GuidColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public BooleanColumnSpecificationBuilder Boolean()
        {
            var boolean = new BooleanColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = boolean;
            return boolean;
        }

        public DateColumnSpecificationBuilder Date()
        {
            var builder = new DateColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public DateTimeColumnSpecificationBuilder DateTime()
        {
            var builder = new DateTimeColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public DateTimeOffsetColumnSpecificationBuilder DateTimeOffset()
        {
            var builder = new DateTimeOffsetColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public TimeOfDayColumnSpecificationBuilder TimeOfDay()
        {
            var builder = new TimeOfDayColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public TimeSpanColumnSpecificationBuilder TimeSpan()
        {
            var builder = new TimeSpanColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public CharColumnSpecificationBuilder Char()
        {
            var builder = new CharColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public SingleColumnSpecificationBuilder Single()
        {
            var builder = new SingleColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public DoubleColumnSpecificationBuilder Double()
        {
            var builder = new DoubleColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public DecimalColumnSpecificationBuilder Decimal()
        {
            var builder = new DecimalColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public StringColumnSpecificationBuilder String()
        {
            var builder = new StringColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public TextColumnSpecificationBuilder Text()
        {
            var builder = new TextColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public JsonColumnSpecificationBuilder Json()
        {
            var builder = new JsonColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public XmlColumnSpecificationBuilder Xml()
        {
            var builder = new XmlColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public BinaryColumnSpecificationBuilder Binary()
        {
            var builder = new BinaryColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public ImageColumnSpecificationBuilder Image()
        {
            var builder = new ImageColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public RowVersionColumnSpecificationBuilder RowVersion()
        {
            var builder = new RowVersionColumnSpecificationBuilder(this);
            ColumnSpecificationBuilder = builder;
            return builder;
        }

        public ColumnBuilder(TableBuilder tableBuilder, int ordinal, string name)
            : base(tableBuilder, ordinal, name)
        {
        }

        internal override bool IsPrimaryKey => false;
    }
}

using NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public class PropertyBuilder : PropertyBuilderBase
    {
        public BytePropertySpecificationBuilder Byte()
        {
            var builder = new BytePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public Int16PropertySpecificationBuilder Int16()
        {
            var builder = new Int16PropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public Int32PropertySpecificationBuilder Int32()
        {
            var builder = new Int32PropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public Int64PropertySpecificationBuilder Int64()
        {
            var builder = new Int64PropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public GuidPropertySpecificationBuilder Guid()
        {
            var builder = new GuidPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public BooleanPropertySpecificationBuilder Boolean()
        {
            var boolean = new BooleanPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = boolean;
            return boolean;
        }

        public DatePropertySpecificationBuilder Date()
        {
            var builder = new DatePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public DateTimePropertySpecificationBuilder DateTime()
        {
            var builder = new DateTimePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public DateTimeOffsetPropertySpecificationBuilder DateTimeOffset()
        {
            var builder = new DateTimeOffsetPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public TimeOfDayPropertySpecificationBuilder TimeOfDay()
        {
            var builder = new TimeOfDayPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public TimeSpanPropertySpecificationBuilder TimeSpan()
        {
            var builder = new TimeSpanPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public CharPropertySpecificationBuilder Char()
        {
            var builder = new CharPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public SinglePropertySpecificationBuilder Single()
        {
            var builder = new SinglePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public DoublePropertySpecificationBuilder Double()
        {
            var builder = new DoublePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public DecimalPropertySpecificationBuilder Decimal()
        {
            var builder = new DecimalPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public StringPropertySpecificationBuilder String()
        {
            var builder = new StringPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public TextPropertySpecificationBuilder Text()
        {
            var builder = new TextPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public JsonPropertySpecificationBuilder Json()
        {
            var builder = new JsonPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public XmlPropertySpecificationBuilder Xml()
        {
            var builder = new XmlPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public BinaryPropertySpecificationBuilder Binary()
        {
            var builder = new BinaryPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public ImagePropertySpecificationBuilder Image()
        {
            var builder = new ImagePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public RowVersionPropertySpecificationBuilder RowVersion()
        {
            var builder = new RowVersionPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PropertyBuilder(EntityBuilder entityBuilder, int ordinal, string name)
            : base(entityBuilder, ordinal, name)
        {
        }

        internal override bool IsPrimaryKey => false;
    }
}

using NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public class PrimaryKeyPropertyBuilder : PropertyBuilderBase
    {
        public PrimaryKeyBytePropertySpecificationBuilder Byte()
        {
            var builder = new PrimaryKeyBytePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyInt16PropertySpecificationBuilder Int16()
        {
            var builder = new PrimaryKeyInt16PropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyInt32PropertySpecificationBuilder Int32()
        {
            var builder = new PrimaryKeyInt32PropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyInt64PropertySpecificationBuilder Int64()
        {
            var builder = new PrimaryKeyInt64PropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyGuidPropertySpecificationBuilder Guid()
        {
            var builder = new PrimaryKeyGuidPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyBooleanPropertySpecificationBuilder Boolean()
        {
            var boolean = new PrimaryKeyBooleanPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = boolean;
            return boolean;
        }

        public PrimaryKeyDatePropertySpecificationBuilder Date()
        {
            var builder = new PrimaryKeyDatePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyDateTimePropertySpecificationBuilder DateTime()
        {
            var builder = new PrimaryKeyDateTimePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyDateTimeOffsetPropertySpecificationBuilder DateTimeOffset()
        {
            var builder = new PrimaryKeyDateTimeOffsetPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyTimeOfDayPropertySpecificationBuilder TimeOfDay()
        {
            var builder = new PrimaryKeyTimeOfDayPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyTimeSpanPropertySpecificationBuilder TimeSpan()
        {
            var builder = new PrimaryKeyTimeSpanPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyCharPropertySpecificationBuilder Char()
        {
            var builder = new PrimaryKeyCharPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeySinglePropertySpecificationBuilder Single()
        {
            var builder = new PrimaryKeySinglePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyDoublePropertySpecificationBuilder Double()
        {
            var builder = new PrimaryKeyDoublePropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyDecimalPropertySpecificationBuilder Decimal()
        {
            var builder = new PrimaryKeyDecimalPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyStringPropertySpecificationBuilder String()
        {
            var builder = new PrimaryKeyStringPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyBinaryPropertySpecificationBuilder Binary()
        {
            var builder = new PrimaryKeyBinaryPropertySpecificationBuilder(this);
            PropertySpecificationBuilder = builder;
            return builder;
        }

        public PrimaryKeyPropertyBuilder(EntityBuilder entityBuilder, int ordinal, string name)
            : base(entityBuilder, ordinal, name)
        {
        }

        internal override bool IsPrimaryKey => true;
    }
}
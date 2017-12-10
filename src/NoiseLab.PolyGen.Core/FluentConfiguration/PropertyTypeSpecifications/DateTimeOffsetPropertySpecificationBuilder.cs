using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class DateTimeOffsetPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new DateTimeOffsetPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DateTimeOffsetPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.DateTimeOffset;

        internal DateTimeOffsetPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
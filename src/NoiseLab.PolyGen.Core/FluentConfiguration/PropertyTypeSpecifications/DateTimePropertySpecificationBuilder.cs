using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class DateTimePropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new DateTimePropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DateTimePropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.DateTime;

        internal DateTimePropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
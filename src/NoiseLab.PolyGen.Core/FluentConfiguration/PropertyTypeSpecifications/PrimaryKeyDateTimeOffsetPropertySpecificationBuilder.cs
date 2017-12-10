using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class PrimaryKeyDateTimeOffsetPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new PrimaryKeyDateTimeOffsetPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.DateTimeOffset;

        internal PrimaryKeyDateTimeOffsetPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
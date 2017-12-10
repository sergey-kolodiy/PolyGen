using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class DoublePropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new DoublePropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DoublePropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Double;

        internal DoublePropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
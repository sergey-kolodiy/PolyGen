using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class PrimaryKeyDoublePropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new PrimaryKeyDoublePropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Double;

        internal PrimaryKeyDoublePropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
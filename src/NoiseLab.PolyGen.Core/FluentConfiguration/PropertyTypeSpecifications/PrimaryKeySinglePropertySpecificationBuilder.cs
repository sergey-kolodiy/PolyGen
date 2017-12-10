using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class PrimaryKeySinglePropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new PrimaryKeySinglePropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Single;

        internal PrimaryKeySinglePropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
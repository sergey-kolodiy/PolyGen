using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class SinglePropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new SinglePropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new SinglePropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Single;

        internal SinglePropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class GuidPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new GuidPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new GuidPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Guid;

        internal GuidPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
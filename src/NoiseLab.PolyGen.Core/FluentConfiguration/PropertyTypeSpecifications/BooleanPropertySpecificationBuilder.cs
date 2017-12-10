using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class BooleanPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new BooleanPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new BooleanPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Boolean;

        internal BooleanPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
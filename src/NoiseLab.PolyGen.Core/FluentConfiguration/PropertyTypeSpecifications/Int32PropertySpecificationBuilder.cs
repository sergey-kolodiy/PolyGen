using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class Int32PropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new Int32PropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new Int32PropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new Int32PropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int32;

        internal Int32PropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
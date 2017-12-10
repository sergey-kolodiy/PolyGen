using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class Int64PropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new Int64PropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new Int64PropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new Int64PropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int64;

        internal Int64PropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
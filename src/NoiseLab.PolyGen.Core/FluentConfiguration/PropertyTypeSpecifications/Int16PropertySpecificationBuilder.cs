using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class Int16PropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new Int16PropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new Int16PropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new Int16PropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int16;

        internal Int16PropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class DecimalPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new DecimalPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DecimalPropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new DecimalPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Decimal;

        internal DecimalPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
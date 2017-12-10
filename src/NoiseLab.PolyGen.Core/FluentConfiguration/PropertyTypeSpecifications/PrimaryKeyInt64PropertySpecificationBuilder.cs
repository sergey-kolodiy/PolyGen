using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class PrimaryKeyInt64PropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new PrimaryKeyInt64PropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new PrimaryKeyInt64PropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int64;

        internal PrimaryKeyInt64PropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
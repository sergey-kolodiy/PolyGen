using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class PrimaryKeyInt16PropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new PrimaryKeyInt16PropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new PrimaryKeyInt16PropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int16;

        internal PrimaryKeyInt16PropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
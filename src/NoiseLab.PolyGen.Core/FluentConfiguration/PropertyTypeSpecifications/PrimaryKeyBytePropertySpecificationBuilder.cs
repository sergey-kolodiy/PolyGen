using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class PrimaryKeyBytePropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new PrimaryKeyBytePropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new PrimaryKeyBytePropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Byte;

        internal PrimaryKeyBytePropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
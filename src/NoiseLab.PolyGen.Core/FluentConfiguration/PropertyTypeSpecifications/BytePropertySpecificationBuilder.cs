using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class BytePropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new BytePropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new BytePropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new BytePropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Byte;

        internal BytePropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
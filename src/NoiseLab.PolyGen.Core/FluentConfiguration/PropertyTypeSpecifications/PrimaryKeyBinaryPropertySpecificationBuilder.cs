using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class PrimaryKeyBinaryPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new PrimaryKeyBinaryPropertySpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new PrimaryKeyBinaryPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Binary;

        internal PrimaryKeyBinaryPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
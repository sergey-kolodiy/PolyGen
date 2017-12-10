using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class TextPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new TextPropertySpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new TextPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new TextPropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new TextPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Text;

        internal TextPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
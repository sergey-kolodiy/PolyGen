using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class XmlPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new XmlPropertySpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new XmlPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new XmlPropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new XmlPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Xml;

        internal XmlPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
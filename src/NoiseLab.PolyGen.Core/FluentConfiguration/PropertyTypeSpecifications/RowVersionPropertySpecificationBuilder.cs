using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class RowVersionPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new RowVersionPropertySpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new RowVersionPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new RowVersionPropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new RowVersionPropertySpecificationBuilder Computed()
        {
            // TODO: Does it make sense to set RowVersion property as Computed?
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.RowVersion;

        internal RowVersionPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}

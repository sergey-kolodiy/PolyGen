using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class CharPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new CharPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new CharPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Char;

        internal CharPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
            // TODO: Is it really required to explicitly set MaxLength for System.Char property?
            MaxLength(1);
        }
    }
}
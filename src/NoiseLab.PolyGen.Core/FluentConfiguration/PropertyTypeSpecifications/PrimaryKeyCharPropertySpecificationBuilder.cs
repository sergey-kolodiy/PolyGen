using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class PrimaryKeyCharPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new PrimaryKeyCharPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Char;

        internal PrimaryKeyCharPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
            // TODO: Is it really required to explicitly set MaxLength for System.Char property?
            MaxLength(1);
        }
    }
}
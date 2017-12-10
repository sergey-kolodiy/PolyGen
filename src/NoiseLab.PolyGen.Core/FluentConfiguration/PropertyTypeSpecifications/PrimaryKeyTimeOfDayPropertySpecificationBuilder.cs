using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class PrimaryKeyTimeOfDayPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new PrimaryKeyTimeOfDayPropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new PrimaryKeyTimeOfDayPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.TimeOfDay;

        internal PrimaryKeyTimeOfDayPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
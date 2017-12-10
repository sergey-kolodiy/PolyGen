using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class TimeOfDayPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new TimeOfDayPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new TimeOfDayPropertySpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new TimeOfDayPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.TimeOfDay;

        internal TimeOfDayPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
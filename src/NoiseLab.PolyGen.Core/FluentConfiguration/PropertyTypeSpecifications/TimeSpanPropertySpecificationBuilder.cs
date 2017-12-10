using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public class TimeSpanPropertySpecificationBuilder : PropertySpecificationBuilderBase
    {
        public new TimeSpanPropertySpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new TimeSpanPropertySpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.TimeSpan;

        internal TimeSpanPropertySpecificationBuilder(PropertyBuilderBase propertyBuilder)
            : base(propertyBuilder)
        {
        }
    }
}
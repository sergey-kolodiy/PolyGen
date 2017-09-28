using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class TimeOfDayColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new TimeOfDayColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new TimeOfDayColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new TimeOfDayColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.TimeOfDay;

        internal TimeOfDayColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
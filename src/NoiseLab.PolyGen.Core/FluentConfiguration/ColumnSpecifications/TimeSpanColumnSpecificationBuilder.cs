using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class TimeSpanColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new TimeSpanColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new TimeSpanColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new TimeSpanColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.TimeSpan;

        internal TimeSpanColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
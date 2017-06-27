using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
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

        internal TimeSpanColumnSpecificationBuilder(ColumnBuilder columnFactory)
            : base(columnFactory)
        {
        }
    }
}
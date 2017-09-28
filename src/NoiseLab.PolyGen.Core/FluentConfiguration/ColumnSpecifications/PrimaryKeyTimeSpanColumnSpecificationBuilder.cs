using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class PrimaryKeyTimeSpanColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new PrimaryKeyTimeSpanColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.TimeSpan;

        internal PrimaryKeyTimeSpanColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
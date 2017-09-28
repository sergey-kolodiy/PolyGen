using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class DateTimeColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new DateTimeColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DateTimeColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.DateTime;

        internal DateTimeColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
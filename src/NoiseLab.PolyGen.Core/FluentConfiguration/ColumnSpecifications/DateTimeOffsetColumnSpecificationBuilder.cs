using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class DateTimeOffsetColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new DateTimeOffsetColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DateTimeOffsetColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new DateTimeOffsetColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.DateTimeOffset;

        internal DateTimeOffsetColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
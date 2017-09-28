using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class PrimaryKeyDateTimeOffsetColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new PrimaryKeyDateTimeOffsetColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.DateTimeOffset;

        internal PrimaryKeyDateTimeOffsetColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
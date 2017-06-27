using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class DateTimeColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new DateTimeColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DateTimeColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new DateTimeColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.DateTime;

        internal DateTimeColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
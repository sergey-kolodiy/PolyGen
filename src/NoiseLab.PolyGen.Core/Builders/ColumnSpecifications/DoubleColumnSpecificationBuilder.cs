using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class DoubleColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new DoubleColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DoubleColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new DoubleColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Double;

        internal DoubleColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class BooleanColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new BooleanColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new BooleanColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new BooleanColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Boolean;

        internal BooleanColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
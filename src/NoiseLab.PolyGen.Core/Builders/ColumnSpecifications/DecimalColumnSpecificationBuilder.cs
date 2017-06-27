using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class DecimalColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new DecimalColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DecimalColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new DecimalColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new DecimalColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Decimal;

        internal DecimalColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
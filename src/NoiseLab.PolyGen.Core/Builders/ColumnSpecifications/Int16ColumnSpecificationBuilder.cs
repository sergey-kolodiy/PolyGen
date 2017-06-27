using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class Int16ColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new Int16ColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new Int16ColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new Int16ColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new Int16ColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int16;

        internal Int16ColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
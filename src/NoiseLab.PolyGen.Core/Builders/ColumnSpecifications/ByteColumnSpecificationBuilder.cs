using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class ByteColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new ByteColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new ByteColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new ByteColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new ByteColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Byte;

        internal ByteColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
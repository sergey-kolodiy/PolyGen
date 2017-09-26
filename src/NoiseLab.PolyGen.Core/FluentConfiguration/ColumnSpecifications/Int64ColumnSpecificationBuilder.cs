using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class Int64ColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new Int64ColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new Int64ColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new Int64ColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new Int64ColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int64;

        internal Int64ColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
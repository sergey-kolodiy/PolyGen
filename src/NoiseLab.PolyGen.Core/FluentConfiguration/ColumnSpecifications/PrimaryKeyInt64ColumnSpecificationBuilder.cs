using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class PrimaryKeyInt64ColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new PrimaryKeyInt64ColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new PrimaryKeyInt64ColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int64;

        internal PrimaryKeyInt64ColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class PrimaryKeyInt32ColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new PrimaryKeyInt32ColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new PrimaryKeyInt32ColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int32;

        internal PrimaryKeyInt32ColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
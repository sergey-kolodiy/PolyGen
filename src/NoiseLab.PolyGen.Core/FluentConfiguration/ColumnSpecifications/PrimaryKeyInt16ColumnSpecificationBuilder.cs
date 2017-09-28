using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class PrimaryKeyInt16ColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new PrimaryKeyInt16ColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new PrimaryKeyInt16ColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int16;

        internal PrimaryKeyInt16ColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
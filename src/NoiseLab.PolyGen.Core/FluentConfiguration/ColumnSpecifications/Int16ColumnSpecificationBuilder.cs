using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class Int16ColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new Int16ColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
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

        internal Int16ColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
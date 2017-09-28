using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class Int32ColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new Int32ColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new Int32ColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new Int32ColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Int32;

        internal Int32ColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
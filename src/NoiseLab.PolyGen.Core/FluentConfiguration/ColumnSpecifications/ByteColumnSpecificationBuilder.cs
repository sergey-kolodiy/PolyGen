using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class ByteColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new ByteColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
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

        internal ByteColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
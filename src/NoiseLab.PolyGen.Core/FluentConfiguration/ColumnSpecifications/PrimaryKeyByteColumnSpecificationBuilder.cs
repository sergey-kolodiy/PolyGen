using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class PrimaryKeyByteColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new PrimaryKeyByteColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new PrimaryKeyByteColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Byte;

        internal PrimaryKeyByteColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
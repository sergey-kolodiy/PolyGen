using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class BinaryColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new BinaryColumnSpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new BinaryColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new BinaryColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Binary;

        internal BinaryColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
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

        internal BinaryColumnSpecificationBuilder(ColumnBuilder columnFactory)
            : base(columnFactory)
        {
        }
    }
}
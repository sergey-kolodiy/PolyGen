using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class ImageColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new ImageColumnSpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new ImageColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new ImageColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Image;

        internal ImageColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
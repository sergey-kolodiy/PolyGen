using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class GuidColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new GuidColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new GuidColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new GuidColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Guid;

        internal GuidColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
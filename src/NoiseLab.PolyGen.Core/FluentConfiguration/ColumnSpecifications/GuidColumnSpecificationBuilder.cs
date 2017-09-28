using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class GuidColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new GuidColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new GuidColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Guid;

        internal GuidColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
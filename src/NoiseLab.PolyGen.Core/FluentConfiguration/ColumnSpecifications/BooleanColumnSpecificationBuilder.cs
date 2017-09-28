using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class BooleanColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new BooleanColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new BooleanColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Boolean;

        internal BooleanColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
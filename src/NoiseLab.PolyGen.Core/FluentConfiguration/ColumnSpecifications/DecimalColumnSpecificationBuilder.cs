using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class DecimalColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new DecimalColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DecimalColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new DecimalColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Decimal;

        internal DecimalColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
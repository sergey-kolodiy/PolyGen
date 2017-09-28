using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class PrimaryKeyDateColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new PrimaryKeyDateColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Date;

        internal PrimaryKeyDateColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
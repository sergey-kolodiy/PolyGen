using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class PrimaryKeyDateTimeColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new PrimaryKeyDateTimeColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.DateTime;

        internal PrimaryKeyDateTimeColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
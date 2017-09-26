using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class DateColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new DateColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new DateColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new DateColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Date;

        internal DateColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
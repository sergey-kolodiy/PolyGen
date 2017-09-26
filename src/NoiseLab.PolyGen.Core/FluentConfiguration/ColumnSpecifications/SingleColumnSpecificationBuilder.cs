using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class SingleColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new SingleColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new SingleColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new SingleColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Single;

        internal SingleColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
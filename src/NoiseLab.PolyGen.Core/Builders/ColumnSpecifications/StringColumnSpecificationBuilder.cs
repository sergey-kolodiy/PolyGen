using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class StringColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new StringColumnSpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new StringColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new StringColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new StringColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new StringColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.String;

        internal StringColumnSpecificationBuilder(ColumnBuilder columnFactory)
            : base(columnFactory)
        {
        }
    }
}